using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketAPI.Models;

namespace TicketAPI.UnitOfWork
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext()
        {
        }

        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options)
        {
        }

        #region old DbSets
        //public DbSet<CodeClient> CodeClients { get; set; }
        //public DbSet<CodePlace> CodePlaces { get; set; }
        //public DbSet<CodeAddress> CodeAddresses { get; set; }
        //public DbSet<AggClientPlaceAddress> AggClientPlaceAddresses { get; set; }
        //public DbSet<AggUserClientPlaceAddress> AggUserClientPlaceAddresses { get; set; }
        //public DbSet<CodeUser> CodeUsers { get; set; }
        //public DbSet<CodeProfile> CodeProfiles { get; set; }
        //public DbSet<ActTicket> ActTickets { get; set; }
        //public DbSet<CodeProblemType> CodeProblemTypes { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //unique constraint for 2 fields
            //modelBuilder.Entity<AggClientPlaceAddress>()
            //    .HasIndex(agg1 => new { agg1.CodePlaceId, agg1.CodeAddressId })
            //    .IsUnique(true);

            #region old code
            //modelBuilder.Entity<CodeClient>().ToTable("CodeClient");
            //modelBuilder.Entity<CodePlace>().ToTable("CodePlace");
            //modelBuilder.Entity<CodeAddress>().ToTable("CodeAddress");
            //modelBuilder.Entity<AggClientPlaceAddress>().ToTable("AggClientPlaceAddress");
            //modelBuilder.Entity<AggUserClientPlaceAddress>().ToTable("AggUserClientPlaceAddress");
            //modelBuilder.Entity<CodeUser>().ToTable("CodeUser");
            //modelBuilder.Entity<CodeProfile>().ToTable("CodeProfile");
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
