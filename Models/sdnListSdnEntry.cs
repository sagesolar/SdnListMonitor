namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/sdnList.xsd")]
    public class sdnListSdnEntry : IEquatable<sdnListSdnEntry>
    {
        private int uidField;

        private string firstNameField;

        private string lastNameField;

        private string titleField;

        private string sdnTypeField;

        private string remarksField;

        private string[] programListField;

        // Had to implement list here as EF does not support collection interface or arrays as given by xsd.exe
        private List<sdnListSdnEntryID> idListField;
                
        private List<sdnListSdnEntryAka> akaListField;
                
        private List<sdnListSdnEntryAddress> addressListField;
                
        private List<sdnListSdnEntryNationality> nationalityListField;
                
        private List<sdnListSdnEntryCitizenship> citizenshipListField;
                
        private List<sdnListSdnEntryDateOfBirthItem> dateOfBirthListField;
                
        private List<sdnListSdnEntryPlaceOfBirthItem> placeOfBirthListField;
                
        private sdnListSdnEntryVesselInfo vesselInfoField;

        /// <remarks/>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uid
        {
            get
            {
                return this.uidField;
            }
            set
            {
                this.uidField = value;
            }
        }

        //public string id { get; set; }

        /// <remarks/>
        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string sdnType
        {
            get
            {
                return this.sdnTypeField;
            }
            set
            {
                this.sdnTypeField = value;
            }
        }

        /// <remarks/>
        public string remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }
        
        // Had to add this to ensure EF can persist the array of strings as pipe(|) delimeted string.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalData { get; set; }

        [NotMapped]
        [System.Xml.Serialization.XmlArrayItemAttribute("program", IsNullable = false)]
        public string[] programList
        {
            get
            {
                return InternalData.Split('|');
            }
            set
            {
                programListField = value;
                InternalData = String.Join("|", programListField.Select(p => p.ToString()).ToArray());
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("id", IsNullable = false)]
        public List<sdnListSdnEntryID> idList
        {
            get
            {
                return this.idListField;
            }
            set
            {
                this.idListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("aka", IsNullable = false)]
        public List<sdnListSdnEntryAka> akaList
        {
            get
            {
                return this.akaListField;
            }
            set
            {
                this.akaListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("address", IsNullable = false)]
        public List<sdnListSdnEntryAddress> addressList
        {
            get
            {
                return this.addressListField;
            }
            set
            {
                this.addressListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("nationality", IsNullable = false)]
        public List<sdnListSdnEntryNationality> nationalityList
        {
            get
            {
                return this.nationalityListField;
            }
            set
            {
                this.nationalityListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("citizenship", IsNullable = false)]
        public List<sdnListSdnEntryCitizenship> citizenshipList
        {
            get
            {
                return this.citizenshipListField;
            }
            set
            {
                this.citizenshipListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("dateOfBirthItem", IsNullable = false)]
        public List<sdnListSdnEntryDateOfBirthItem> dateOfBirthList
        {
            get
            {
                return this.dateOfBirthListField;
            }
            set
            {
                this.dateOfBirthListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("placeOfBirthItem", IsNullable = false)]
        public List<sdnListSdnEntryPlaceOfBirthItem> placeOfBirthList
        {
            get
            {
                return this.placeOfBirthListField;
            }
            set
            {
                this.placeOfBirthListField = value;
            }
        }

        /// <remarks/>
        public sdnListSdnEntryVesselInfo vesselInfo
        {
            get
            {
                return this.vesselInfoField;
            }
            set
            {
                this.vesselInfoField = value;
            }
        }

        public bool Equals(sdnListSdnEntry obj)
        {
          return obj is sdnListSdnEntry entry &&
          uid == entry.uid &&
          firstName == entry.firstName &&
          lastName == entry.lastName &&
          title == entry.title &&
          sdnType == entry.sdnType &&
          remarks == entry.remarks &&
          InternalData == entry.InternalData && ((vesselInfo == null && entry.vesselInfo == null) || vesselInfo.Equals(entry.vesselInfo)) &&
          idList.All(x => x.Equals(obj?.idList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault())) &&
          akaList.All(x => x.Equals(obj?.akaList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault())) &&
          addressList.All(x => x.Equals(obj?.addressList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault())) &&
          nationalityList.All(x => x.Equals(obj?.nationalityList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault())) &&
          citizenshipList.All(x => x.Equals(obj?.citizenshipList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault())) &&
          dateOfBirthList.All(x => x.Equals(obj?.dateOfBirthList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault())) &&
          placeOfBirthList.All(x => x.Equals(obj?.placeOfBirthList?.Where(y => y.uid.Equals(y.uid)).FirstOrDefault()));
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(uid);
            hash.Add(firstName);
            hash.Add(lastName);
            hash.Add(title);
            hash.Add(sdnType);
            hash.Add(remarks);
            hash.Add(InternalData);
            hash.Add(idList);
            hash.Add(akaList);
            hash.Add(addressList);
            hash.Add(nationalityList);
            hash.Add(citizenshipList);
            hash.Add(dateOfBirthList);
            hash.Add(placeOfBirthList);
            hash.Add(vesselInfo);
            return hash.ToHashCode();
        }
    }
}
