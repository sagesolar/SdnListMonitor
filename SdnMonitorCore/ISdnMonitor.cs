namespace SdnMonitorCore
{
    using Models;
    using System.Threading.Tasks;

    interface ISdnMonitor
    {
        /// <summary>
        /// Attempts to compare the remote SDN list to the most recently retrieved version of the the SDN List.
        /// If the list has never been retrieved before, it persists the list. 
        /// Otherwise the existing list is updated after the comparison has occurred.
        /// </summary>
        /// <returns>A summary of any changes that have been determined from comparison of the local and remote SDN Lists.</returns>
        Task<SdnChangeSummary> CompareSdnListAsync();
    }
}
