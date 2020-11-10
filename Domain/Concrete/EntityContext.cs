using TicketManager.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Diagnostics;

namespace TicketManager.Domain.Concrete
{
    public class EntityContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        public EntityContext()
        {
            //TODO Output SQL to output that is not Debug
            this.Database.Log = sql => Trace.WriteLine(sql);
        }
    }
}
