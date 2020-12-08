namespace SdnMonitorData
{
    using Models;
    using System.Threading.Tasks;

    interface IDataEngine
    {
        /// <summary>
        /// Creates a new SDN List record in the SDN List database, using the given SDN List record.
        /// </summary>
        /// <param name="sdnListRecord">The SDN List record to persist.</param>
        /// <returns>The number of affected records.</returns>
        Task<int> CreateSdnListRecordAsync(sdnList sdnListRecord);

        /// <summary>
        /// Retrieves the SDN List record from the SDN List database, along with all subordinate records.
        /// As this database currently only stores one SDN List record, 
        /// this is the only record there is to return.
        /// </summary>
        /// <returns>The SDN List record, if any exists.</returns>
        Task<sdnList> RetrieveSdnRecordAsync();

        /// <summary>
        /// Creates a new SDN List Entry record from the given model.
        /// </summary>
        /// <param name="newSdnEntryRecord">The given SDN Entry model.</param>
        /// <returns>The number of affected records.</returns>
        Task<int> CreateSdnEntryRecordAsync(sdnListSdnEntry newSdnEntryRecord);

        /// <summary>
        /// Updates a given SDN Entry record in the SDN List database.
        /// </summary>
        /// <param name="sdnList">The SDN Entry record to update.</param>
        /// <returns>The number of affected records.</returns>
        Task<int> UpdateSdnEntryRecordAsync(sdnListSdnEntry updatedSdnEntryRecord);

        /// <summary>
        /// Deletes an SDN Entry record from the SDN list database that corresponds to the given Id.
        /// </summary>
        /// <param name="sdnEntryRecordIdToRemove">The SDN List model to delete.</param>
        /// <returns>The number of affected records.</returns>
        Task<int> DeleteSdnEntryRecordAsync(int sdnEntryRecordIdToRemove);

        /// <summary>
        /// Updates the existing SDN List's Publish Information record.
        /// </summary>
        /// <param name="updatedPublishInfoRecord"></param>
        /// <returns>The number of affected records.</returns>
        Task<int> UpdateSndListPublishInfoAsync(sdnListPublshInformation updatedPublishInfoRecord);
    }
}
