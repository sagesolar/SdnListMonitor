namespace SdnMonitorData
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A class for holding database operation methods.
    /// </summary>
    public class DataEngine : IDataEngine
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private readonly SdnListContext context;

        /// <summary>
        /// Initialises a new instance of the <see cref="DataEngine" /> class.
        /// </summary>
        /// <param name="context">The db context.</param>
        public DataEngine(SdnListContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<int> CreateSdnListRecordAsync(sdnList sdnListRecord)
        {
            this.context.sdnLists.Add(sdnListRecord);
            return await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<sdnList> RetrieveSdnRecordAsync()
        {
            return await context.sdnLists
                .Include(x => x.publshInformation)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.addressList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.akaList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.citizenshipList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.dateOfBirthList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.idList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.nationalityList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.placeOfBirthList)
                .Include(x => x.sdnEntry)
                    .ThenInclude(y => y.vesselInfo)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<int> CreateSdnEntryRecordAsync(sdnListSdnEntry newSdnEntryRecord)
        {
            var sdnListRecord = await this.context.sdnLists.FirstOrDefaultAsync();
           
            if (sdnListRecord != null)
            {
                sdnListRecord.sdnEntry.Add(newSdnEntryRecord);
            }           
            
            return await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateSdnEntryRecordAsync(sdnListSdnEntry updatedSdnEntryRecord)
        {
            var sdnListRecord = await this.context.sdnLists.FirstOrDefaultAsync();

            if (sdnListRecord != null)
            {
                var sdnEntryToUpdate = sdnListRecord.sdnEntry.Where(x => x.uid == updatedSdnEntryRecord.uid).FirstOrDefault();

                // TODO: This will need to be done better in future, either by EF change tracking or by independent value setting,
                // similar to the method used in 'UpdateSndListPublishInfoAsync'.
                // This will be particularly needed if different versions of the overall SDN List are stored.
                if (sdnEntryToUpdate != null)
                {
                    // Cannot use .Remove() here as that does not actually delete the record, 
                    // as in a EF disconnected state it just flags it as removed from the context.

                    // TODO : This is not working, as the foreign keys on all entity records related to SDN Entry records
                    // need to be exposed in their related domain models, and then cascade delete rules need to be configured
                    // in the DB context ModelBuilder.
                    this.context.Entry(sdnEntryToUpdate).State = EntityState.Deleted;
                    await this.context.SaveChangesAsync();

                    sdnListRecord.sdnEntry.Add(updatedSdnEntryRecord);
                }
            }

            return await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeleteSdnEntryRecordAsync(int sdnEntryRecordIdToRemove)
        {
            var sdnListRecord = await this.context.sdnLists.FirstOrDefaultAsync();

            if (sdnListRecord != null)
            {
                var sdnEntryToDelete = sdnListRecord.sdnEntry.Where(x => x.uid == sdnEntryRecordIdToRemove).FirstOrDefault();

                if (sdnEntryToDelete != null)
                {                    
                    // Cannot use .Remove() here as that does not actually delete the record, 
                    // as in a EF disconnected state it just flags it as removed from the context.
                    this.context.Entry(sdnEntryToDelete).State = EntityState.Deleted;
                }                
            }

            return await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> UpdateSndListPublishInfoAsync(sdnListPublshInformation updatedPublishInfoRecord)
        {
            var sdnListRecord = await this.context.sdnLists.FirstOrDefaultAsync();

            if (sdnListRecord != null)
            {
                sdnListRecord.publshInformation.Publish_Date = updatedPublishInfoRecord.Publish_Date;
                sdnListRecord.publshInformation.Record_Count = updatedPublishInfoRecord.Record_Count;
                sdnListRecord.publshInformation.Record_CountSpecified = updatedPublishInfoRecord.Record_CountSpecified;
            }
            
            return await context.SaveChangesAsync();
        }
    }
}
