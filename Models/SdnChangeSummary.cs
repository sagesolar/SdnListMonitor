namespace Models
{
    using System;

    /// <summary>
    /// A model of the SDN list change summary, returned to the consumer.
    /// </summary>
    public class SdnChangeSummary
    {
        /// <summary>
        /// Gets or set the publish date of the SDN list.
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the total count of SDN List Entries.
        /// Using int here to rely on default(int) being 0.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Gets or sets the added count of SDN List Entries.
        /// Using int here to rely on default(int) being 0.
        /// </summary>
        public int AddedRecordsCount { get; set; }

        /// <summary>
        /// Gets or sets the modified count of SDN List Entries.
        /// Using int here to rely on default(int) being 0.
        /// </summary>
        public int ModifiedRecordsCount { get; set; }

        /// <summary>
        /// Gets or sets the removed count of SDN List Entries.
        /// Using int here to rely on default(int) being 0.
        /// </summary>
        public int RemovedRecordsCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an SDN List record was available.
        /// Defaults to false.
        /// This value can be used to show a warning when no record is available.
        /// </summary>
        public bool IsRecordAvailable { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the SDN List statistics are from a cached version of the SDN List.
        /// Defaults to false, meaning that the statistics where calculated using a live version of the list.
        /// </summary>
        public bool IsFromCachedData { get; set; } = false;
    }
}
