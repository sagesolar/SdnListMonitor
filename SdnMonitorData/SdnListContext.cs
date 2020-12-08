namespace SdnMonitorData
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    /// <summary>
    /// The SDN List database context clasee.
    /// </summary>
    public class SdnListContext : DbContext
    {        
        /// <summary>
        ///  The main sdnLists collection.
        /// </summary>
        public DbSet<sdnList> sdnLists { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="SdnListContext" /> class.
        /// </summary>
        public SdnListContext(DbContextOptions<SdnListContext> options): base(options)
        {
        }

        /// <summary>
        /// Ensure that SDN List provided keys are not auto-generated, and that custom ones are.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sdnList>().Property(t => t.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<sdnListPublshInformation>().Property(t => t.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<sdnListSdnEntryVesselInfo>().Property(t => t.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<sdnListSdnEntry>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryAddress>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryAka>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryCitizenship>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryDateOfBirthItem>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryID>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryNationality>().Property(t => t.uid).ValueGeneratedNever();
            modelBuilder.Entity<sdnListSdnEntryPlaceOfBirthItem>().Property(t => t.uid).ValueGeneratedNever();
        }        
    }
}
