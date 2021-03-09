using TicketManager.Domain.Entities;
using System.Data.Entity;

namespace TicketManager.Domain.Concrete
{
    public class EntityContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<LoginInformation> LoginInformations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserCartInformation> CartInformation { get; set; }

        public EntityContext():base("name=EntityContext")
        {
            //TODO Output SQL to output that is not Debug
            //this.Database.Log = sql => Trace.WriteLine(sql);
            Database.SetInitializer<EntityContext>(new DropCreateDatabaseIfModelChanges<EntityContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure Column
            modelBuilder.Entity<Ticket>()
                        .Property(p => p.StartBuyTime)
                        .HasColumnName("StartBuyTime")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<Ticket>()
                        .Property(p => p.EndBuyTime)
                        .HasColumnName("EndBuyTime")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<Ticket>()
                        .Property(p => p.EventTime)
                        .HasColumnName("EventTime")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<UserCartInformation>()
                        .Property(p => p.DateAdded)
                        .HasColumnName("DateAdded")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<UserCartInformation>()
                        .Property(p => p.CheckOutTime)
                        .HasColumnName("CheckOutTime")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<Transaction>()
                        .Property(p => p.DateMade)
                        .HasColumnName("DateMade")
                        .HasColumnType("datetime2");

        }
    }
}
