using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Concrete
{
    public class EFTicketRepository : ITicketRepository
    {
        private EntityContext context = new EntityContext();

        public IQueryable<Ticket> Tickets
        {
            get { return context.Tickets; }
        }

        public void SaveTicket( Ticket tick)
        {
            if(tick.TicketID == 0)
            {
                context.Tickets.Add(tick);
            }
            else
            {
                Ticket Entry = context.Tickets.Find(tick.TicketID);
                if(Entry != null)
                {
                    Entry.TicketName = tick.TicketName;
                    Entry.Description = tick.Description;
                    Entry.Organizer = tick.Organizer;
                    Entry.EventTime = tick.EventTime;
                    Entry.StartBuyTime = tick.StartBuyTime;
                    Entry.EndBuyTime = tick.EndBuyTime;
                    Entry.Price = tick.Price;
                }
            }
            context.SaveChanges();
        }

        public Ticket DeleteTicket(int tickId)
        {
            Ticket Entry = context.Tickets.Find(tickId);
            if(Entry != null)
            {
                context.Tickets.Remove(Entry);
                context.SaveChanges();
            }
            return Entry;
        }

        public int GetSize()
        {
            return context.Tickets.Count();
        }

        public IQueryable<Ticket> GetSpecificPage(int pageSize, int pageNumber)
        {
            return context.Tickets.OrderBy(p => p.TicketID).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
