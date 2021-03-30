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
        public DbSet<ShoppingCarts> CartInformation { get; set; }

        public EntityContext():base("name=EntityContext")
        {
            Database.SetInitializer<EntityContext>(new DropCreateDatabaseIfModelChanges<EntityContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure Column
            modelBuilder.Entity<Ticket>()
                        .Property(p => p.StartingDateAvailable)
                        .HasColumnName("StartingDateAvailable")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<Ticket>()
                        .Property(p => p.EndDateAvailable)
                        .HasColumnName("EndDateAvailable")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<Ticket>()
                        .Property(p => p.TimeOfEvent)
                        .HasColumnName("TimeOfEvent")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<ShoppingCarts>()
                        .Property(p => p.DateAdded)
                        .HasColumnName("DateAdded")
                        .HasColumnType("datetime2");

            modelBuilder.Entity<ShoppingCarts>()
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
