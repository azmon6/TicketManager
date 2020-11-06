using TicketManager.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace TicketManager.Domain.Concrete
{
    public class EntityContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }
}
