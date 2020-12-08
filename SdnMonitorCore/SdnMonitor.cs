namespace SdnMonitorCore
{
    using Models;
    using SdnMonitorData;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using System.Globalization;

    /// <summary>
    /// A class for monitoring the remote SDN List and updating the local version.
    /// </summary>
    public class SdnMonitor :ISdnMonitor
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly SdnListContext context;

        /// <summary>
        /// The retrieved version of the SDN List.
        /// </summary>
        private sdnList retrievedSdnList;

        /// <summary>
        /// A collection of SDN Entries to be created in the local db.
        /// </summary>
        private IEnumerable<sdnListSdnEntry> sdnEntriesToCreate;

        /// <summary>
        /// A collection of SDN Entries to be deleted from the local db.
        /// </summary>
        private IEnumerable<sdnListSdnEntry> sdnEntriesToDelete;

        /// <summary>
        /// A collection of SDN Entries to be updated in the local db.
        /// </summary>
        private IEnumerable<sdnListSdnEntry> sdnEntriesToUpdate;

        /// <summary>
        /// The SDN Change summary model.
        /// </summary>
        private SdnChangeSummary sdnChangeSummary;

        /// <summary>
        /// The URL of the remote SDN List xml resource. 
        /// </summary>
        private string sdnListUrl;

        public SdnMonitor(SdnListContext context)
        {
            this.context = context;
            this.sdnEntriesToCreate = new List<sdnListSdnEntry>();
            this.sdnEntriesToDelete = new List<sdnListSdnEntry>();
            this.sdnEntriesToUpdate = new List<sdnListSdnEntry>();
            this.sdnChangeSummary = new SdnChangeSummary();

            // TODO: This needs to be moved to appsettings, and accessed from there.
            this.sdnListUrl = "https://www.treasury.gov/ofac/downloads/sdn.xml";
        }

        /// <summary>
        /// Attempts to retrieve the the remote SDN List resource, and then deserialises it into a domain model.
        /// </summary>
        /// <returns>The SDN List domain model representation of the retrieved SDN List resource (XML).</returns>
        private async Task<sdnList> RetrieveSdnListAsync()
        {
            var result = new sdnList();
            var returnedXml = string.Empty;

            // Attempt to retrieve the remote SDN List resource.
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("text/xml"));

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.sdnListUrl);
                var httpResult = await client.SendAsync(httpRequest);

                if (httpResult.IsSuccessStatusCode)
                {
                    returnedXml = await httpResult.Content.ReadAsStringAsync();
                }
            }

            // Ensure the returned resource is not null.
            if (!string.IsNullOrWhiteSpace(returnedXml))
            {
                var serializer = new XmlSerializer(typeof(sdnList));

                using (var reader = new StringReader(returnedXml))
                {
                    result = (sdnList)serializer.Deserialize(reader);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<SdnChangeSummary> CompareSdnListAsync()
        {
            var de = new DataEngine(this.context);

            var existingSdnListTask = de.RetrieveSdnRecordAsync();
            var retrievedSdnListTask = this.RetrieveSdnListAsync();

            existingSdnListTask.Wait();
            retrievedSdnListTask.Wait();

            var existingSdnList = existingSdnListTask.Result;
            this.retrievedSdnList = retrievedSdnListTask.Result;

            // Was the SDN List succesfully retrieved and deserialised?
            if (this.retrievedSdnList != null)
            {
                this.sdnChangeSummary.PublishDate = GetSdnListPublishDateTime(this.retrievedSdnList.publshInformation.Publish_Date);
                this.sdnChangeSummary.TotalRecordsCount = this.retrievedSdnList.publshInformation.Record_Count;
                this.sdnChangeSummary.IsRecordAvailable = true;

                // The SDN List has never been added to the local database.
                if (existingSdnList == null)
                {
                    // No need to wait here, as no comparison stats with a local SDN list record are available.
                    await de.CreateSdnListRecordAsync(this.retrievedSdnList);
                    sdnChangeSummary.AddedRecordsCount = this.retrievedSdnList.sdnEntry.Count;
                }

                // Compare existing SDN List with retrieved version of the List.
                else
                {
                    this.CompareRetrievedWithExistingSdnList(this.retrievedSdnList, existingSdnList);
                    await this.UpdateLocalSdnListAsync();
                }
            }

            // No remote SDN List was available, so just return the local one.
            else if (existingSdnList != null)
            {
                this.sdnChangeSummary.PublishDate = GetSdnListPublishDateTime(existingSdnList.publshInformation.Publish_Date);
                this.sdnChangeSummary.TotalRecordsCount = existingSdnList.publshInformation.Record_Count;
                this.sdnChangeSummary.IsRecordAvailable = true;
                this.sdnChangeSummary.IsFromCachedData = true;                
            }

            return this.sdnChangeSummary;
        }

        /// <summary>
        /// Attempts to parse the given SDN List Publish Date string into a DateTime.
        /// Converting the string into a datetime makes the returned information richer with computed date record.
        /// </summary>
        /// <param name="sdnListPublishDate">The given SDN List publish date string.</param>
        /// <returns>The parsed DateTime, or null if no date/unable to parse.</returns>
        private DateTime? GetSdnListPublishDateTime(string sdnListPublishDate)
        {
            DateTime? result = null;

            if (DateTime.TryParseExact(sdnListPublishDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newPublishDate))
            {
                result = newPublishDate;
            }

            return result;
        }

        /// <summary>
        /// Compares the retrieved SDN List with the existing, local SDN List. If any additions, deletions or modifications are detected,
        /// the affected records are logged and their counts are added to the SDN record summary.
        /// </summary>
        /// <param name="retrievedSdnList"></param>
        /// <param name="existingSdnList"></param>
        private void CompareRetrievedWithExistingSdnList(sdnList retrievedSdnList, sdnList existingSdnList)
        {
            // If the two SDN lists have the same record count and publish date, consider them as identical.
            // This has been used as quick to avoid labourious comparison of the lists, however more investigation of 
            // the nature of the SDN list would be needed to ensure that these two values can be relied on for this purpose.
            if (retrievedSdnList.publshInformation.Record_Count == existingSdnList.publshInformation.Record_Count &&
                retrievedSdnList.publshInformation.Publish_Date == existingSdnList.publshInformation.Publish_Date)
            {
                return;
            }

            // Find any added and/or removed SDN Entry records between retrieved (r) and existing (e) SDN Lists.
            this.sdnEntriesToCreate = retrievedSdnList.sdnEntry.Where(r => existingSdnList.sdnEntry.All(e => e.uid != r.uid));
            this.sdnEntriesToDelete = existingSdnList.sdnEntry.Where(e => retrievedSdnList.sdnEntry.All(r => r.uid != e.uid));

            // Compare the retrieved (r) and existing (e) SDN Entry records between retrieved (r) and existing (e) SDN Lists,
            // and find any modified SDN Entry records.
            this.sdnEntriesToUpdate = retrievedSdnList.sdnEntry
                // Ensure that this comparison is only on records that are in both the retrieved and existings records.
                .Where(r => existingSdnList.sdnEntry.Select(e => e.uid).Contains(r.uid))
                // Join up retrieved and existing records, and find any that have changed shape.
                .Where(r => !r.Equals(existingSdnList.sdnEntry.Where(e => e.uid.Equals(r.uid)).First()));

            // Set the counts from this comparison on the summary.
            this.sdnChangeSummary.AddedRecordsCount = this.sdnEntriesToCreate.Count();
            this.sdnChangeSummary.RemovedRecordsCount = this.sdnEntriesToDelete.Count();
            this.sdnChangeSummary.ModifiedRecordsCount = this.sdnEntriesToUpdate.Count();
        }

        /// <summary>
        /// Updates the existing version the SDN List record to include any additions/deletions/modifications 
        /// to the list that have beend detected from comparing the existing SDN List and the retrieved version.
        /// </summary>
        /// <returns>A related task.</returns>
        private async Task UpdateLocalSdnListAsync()
        {
            var de = new DataEngine(this.context);

            // Have any changes occured to the SDN List's SDN Entry records?
            if (sdnEntriesToCreate.Any() || sdnEntriesToUpdate.Any() || sdnEntriesToDelete.Any())
            {
                // Ensure the parent SDN List record's Publish Information is updated.
                await de.UpdateSndListPublishInfoAsync(this.retrievedSdnList.publshInformation);

                if (sdnEntriesToCreate.Any())
                {
                    foreach (var sdnEntry in sdnEntriesToCreate)
                    {
                        await de.CreateSdnEntryRecordAsync(sdnEntry);
                    }
                }

                if (sdnEntriesToUpdate.Any())
                {
                    foreach (var sdnEntry in sdnEntriesToUpdate)
                    {
                        await de.UpdateSdnEntryRecordAsync(sdnEntry);
                    }
                }

                if (sdnEntriesToDelete.Any())
                {
                    foreach (var sdnEntry in sdnEntriesToDelete.ToList())
                    {
                        await de.DeleteSdnEntryRecordAsync(sdnEntry.uid);
                    }
                }
            }
        }

        /// <summary>
        /// This method is used for testing the process of comparing two SDN records, 
        /// and persisting any changes between their SDN Entry records.
        /// Change the 'StringReader' payload to pull in different versions of the list.
        /// </summary>
        /// <returns>The SDN List domain model representation of the test SDN List resource (XML).</returns>
        private sdnList RetrieveSampleSdnList()
        {
            // TODO: These test XML samples need to be converted into unit tests.
            var sampleXml =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>11/30/2019</Publish_Date>
                        <Record_Count>2</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>AEROCARIBBEAN AIRLINES</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry>                    
                    <sdnEntry>
                        <uid>15680</uid>
                        <lastName>DESARROLLOS BIO GAS, S.A. DE C.V.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>SYRIA</program>
                            <program>SDGT</program>
                        </programList>
                        <idList>
                            <id>
                                <uid>8476</uid>
                                <idType>R.F.C.</idType>
                                <idNumber>DBG0805095P7</idNumber>
                                <idCountry>Mexico</idCountry>
                            </id>
                        </idList>
                        <addressList>
                            <address>
                                <uid>23552</uid>
                                <address1>Independencia Sur No. 185</address1>
                                <address2>Col. Analco</address2>
                                <city>Guadalajara</city>
                                <stateOrProvince>Jalisco</stateOrProvince>
                                <postalCode>C.P. 44450</postalCode>
                                <country>Mexico</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                </sdnList>";

            // Changed AEROCARIBBEAN AIRLINES = BRITISH AIRWAYS on record (36).
            var sampleXmlChangedOne =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>12/10/2019</Publish_Date>
                        <Record_Count>2</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>BRITISH AIRWAYS</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry>                    
                    <sdnEntry>
                        <uid>15680</uid>
                        <lastName>DESARROLLOS BIO GAS, S.A. DE C.V.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>SYRIA</program>
                            <program>SDGT</program>
                        </programList>
                        <idList>
                            <id>
                                <uid>8476</uid>
                                <idType>R.F.C.</idType>
                                <idNumber>DBG0805095P7</idNumber>
                                <idCountry>Mexico</idCountry>
                            </id>
                        </idList>
                        <addressList>
                            <address>
                                <uid>23552</uid>
                                <address1>Independencia Sur No. 185</address1>
                                <address2>Col. Analco</address2>
                                <city>Guadalajara</city>
                                <stateOrProvince>Jalisco</stateOrProvince>
                                <postalCode>C.P. 44450</postalCode>
                                <country>Mexico</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                </sdnList>";

            // Added record (173).
            var sampleXmlAddedOne =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>12/02/2019</Publish_Date>
                        <Record_Count>3</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>AEROCARIBBEAN AIRLINES</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                    <sdnEntry>
                        <uid>15680</uid>
                        <lastName>DESARROLLOS BIO GAS, S.A. DE C.V.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>SYRIA</program>
                            <program>SDGT</program>
                        </programList>
                        <idList>
                            <id>
                                <uid>8476</uid>
                                <idType>R.F.C.</idType>
                                <idNumber>DBG0805095P7</idNumber>
                                <idCountry>Mexico</idCountry>
                            </id>
                        </idList>
                        <addressList>
                            <address>
                                <uid>23552</uid>
                                <address1>Independencia Sur No. 185</address1>
                                <address2>Col. Analco</address2>
                                <city>Guadalajara</city>
                                <stateOrProvince>Jalisco</stateOrProvince>
                                <postalCode>C.P. 44450</postalCode>
                                <country>Mexico</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                    <sdnEntry>
                        <uid>173</uid>
                        <lastName>ANGLO-CARIBBEAN CO., LTD.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>57</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AVIA IMPORT</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>129</uid>
                                <address1>Ibex House, The Minories</address1>
                                <city>London</city>
                                <postalCode>EC3N 1DY</postalCode>
                                <country>United Kingdom</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                </sdnList>";

            // Added record (173) & changed AEROCARIBBEAN AIRLINES = BRITISH AIRWAYS on record (36).
            var sampleXmlAddedOneChangedOne =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>12/03/2019</Publish_Date>
                        <Record_Count>3</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>BRITISH AIRWAYS</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry>                    
                    <sdnEntry>
                        <uid>15680</uid>
                        <lastName>DESARROLLOS BIO GAS, S.A. DE C.V.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>SYRIA</program>
                            <program>SDGT</program>
                        </programList>
                        <idList>
                            <id>
                                <uid>8476</uid>
                                <idType>R.F.C.</idType>
                                <idNumber>DBG0805095P7</idNumber>
                                <idCountry>Mexico</idCountry>
                            </id>
                        </idList>
                        <addressList>
                            <address>
                                <uid>23552</uid>
                                <address1>Independencia Sur No. 185</address1>
                                <address2>Col. Analco</address2>
                                <city>Guadalajara</city>
                                <stateOrProvince>Jalisco</stateOrProvince>
                                <postalCode>C.P. 44450</postalCode>
                                <country>Mexico</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                    <sdnEntry>
                        <uid>173</uid>
                        <lastName>ANGLO-CARIBBEAN CO., LTD.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>57</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AVIA IMPORT</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>129</uid>
                                <address1>Ibex House, The Minories</address1>
                                <city>London</city>
                                <postalCode>EC3N 1DY</postalCode>
                                <country>United Kingdom</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                </sdnList>";

            // Added record (173) & removed record (15680).
            var sampleXmlAddedOneRemovedOne =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>12/04/2019</Publish_Date>
                        <Record_Count>2</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>AEROCARIBBEAN AIRLINES</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                    <sdnEntry>
                        <uid>173</uid>
                        <lastName>ANGLO-CARIBBEAN CO., LTD.</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>57</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AVIA IMPORT</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>129</uid>
                                <address1>Ibex House, The Minories</address1>
                                <city>London</city>
                                <postalCode>EC3N 1DY</postalCode>
                                <country>United Kingdom</country>
                            </address>
                        </addressList>
                    </sdnEntry>
                </sdnList>";

            // Removed record (15680).
            var sampleXmlRemovedOne =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>12/22/2019</Publish_Date>
                        <Record_Count>1</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>AEROCARIBBEAN AIRLINES</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry> 
                </sdnList>";

            // Removed record (15680) & changed AEROCARIBBEAN AIRLINES = BRITISH AIRWAYS on record (36).
            var sampleXmlRemovedOneChangeOne =
                @"<?xml version='1.0' standalone='yes'?>
                <sdnList xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns='http://tempuri.org/sdnList.xsd'>
                    <publshInformation>
                        <Publish_Date>12/06/2019</Publish_Date>
                        <Record_Count>1</Record_Count>
                    </publshInformation>
                    <sdnEntry>
                        <uid>36</uid>
                        <lastName>BRITISH AIRWAYS</lastName>
                        <sdnType>Entity</sdnType>
                        <programList>
                            <program>CUBA</program>
                        </programList>
                        <akaList>
                            <aka>
                                <uid>12</uid>
                                <type>a.k.a.</type>
                                <category>strong</category>
                                <lastName>AERO-CARIBBEAN</lastName>
                            </aka>
                        </akaList>
                        <addressList>
                            <address>
                                <uid>25</uid>
                                <city>Havana</city>
                                <country>Cuba</country>
                            </address>
                        </addressList>
                    </sdnEntry> 
                </sdnList>";

            var serializer = new XmlSerializer(typeof(sdnList));
            sdnList result;

            using (var reader = new StringReader(sampleXmlChangedOne))
            {
                result = (sdnList)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}