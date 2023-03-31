using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;

namespace TicketAPI.UnitOfWork
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) 
            : base(options)
        { }

        public DbSet<CodeClient> CodeClient { get; set; }
        public DbSet<CodePlace> CodePlace { get; set; }
        public DbSet<CodeAddress> CodeAddress { get; set; }
        public DbSet<AggClientPlaceAddress> AggClientPlaceAddress { get; set; }
        public DbSet<AggUserClientPlaceAddress> AggUserClientPlaceAddress { get; set; }
        public DbSet<CodeUser> CodeUser { get; set; }
        public DbSet<ActTicket> ActTicket { get; set; }
        public DbSet<CodeProblemType> CodeProblemType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region old code
            //unique constraint for 2 fields
            //modelBuilder.Entity<AggClientPlaceAddress>()
            //    .HasIndex(agg1 => new { agg1.CodePlaceId, agg1.CodeAddressId })
            //    .IsUnique(true);
            //modelBuilder.Entity<CodeClient>().ToTable("CodeClient");
            //modelBuilder.Entity<CodePlace>().ToTable("CodePlace");
            //modelBuilder.Entity<CodeAddress>().ToTable("CodeAddress");
            //modelBuilder.Entity<AggClientPlaceAddress>().ToTable("AggClientPlaceAddress");
            //modelBuilder.Entity<AggUserClientPlaceAddress>().ToTable("AggUserClientPlaceAddress");
            //modelBuilder.Entity<CodeUser>().ToTable("CodeUser");
            //modelBuilder.Entity<ActTicket>().ToTable("ActTicket");
            //modelBuilder.Entity<CodeProblemType>().ToTable("CodeProblemType");

            //Promena default datetime2 to datetime:
            //modelBuilder.Entity<ActTicket>()
            //    .Property(t => t.CreatedDate)
            //    .HasColumnType("datetime");
            //modelBuilder.Entity<ActTicket>()
            //    .Property(t => t.StartedDate)
            //    .HasColumnType("datetime");
            //modelBuilder.Entity<ActTicket>()
            //    .Property(t => t.FinishedDate)
            //    .HasColumnType("datetime");
            //Promena byte na bool
            //modelBuilder.Entity<CodeClient>()
            //    .Property(cc => cc.CreatedAutomatic)
            //    .HasColumnType("bit");
            //modelBuilder.Entity<CodePlace>()
            //    .Property(cp => cp.CreatedAutomatic)
            //    .HasColumnType("bit");
            //modelBuilder.Entity<CodeAddress>()
            //    .Property(ca => ca.CreatedAutomatic)
            //    .HasColumnType("bit");
            //modelBuilder.Entity<CodeUser>()
            //    .Property(cu => cu.CreatedAutomatic)
            //    .HasColumnType("bit");
            #endregion
        }
    }
}
