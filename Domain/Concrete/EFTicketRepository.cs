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

        public bool SaveTicket( Ticket tick)
        {
            //TODO Try catch online implementation
            if(tick.TicketID == 0)
            {
                context.Tickets.Add(tick);
            }
            else
            {
                Ticket Entry = context.Tickets.Find(tick.TicketID);
                if(Entry != null)
                {
                    if (!Entry.RowVersion.SequenceEqual(tick.RowVersion))
                    {
                        tick.RowVersion = Entry.RowVersion;
                        return false;
                    }

                    Entry.TicketName = tick.TicketName;
                    Entry.Description = tick.Description;
                    Entry.Organizer = tick.Organizer;
                    Entry.EventTime = tick.EventTime;
                    Entry.StartBuyTime = tick.StartBuyTime;
                    Entry.EndBuyTime = tick.EndBuyTime;
                    Entry.Price = tick.Price;
                    Entry.AmountRemaining = tick.AmountRemaining;
                    
                }
            }

            context.SaveChanges();
            return true;
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

        public IQueryable<Ticket> GetSpecificPage(int pageSize, int pageNumber, string orderByWhat = "EventTime", string asc = "False")
        {
            var temp = context.Tickets.AsQueryable();
            if (asc == "True")
            {
                switch (orderByWhat)
                {
                    case null:
                        temp = temp.OrderBy(p => p.EventTime);
                        break;
                    case "TicketName":
                        temp = temp.OrderBy(p => p.TicketName);
                        break;
                    case "Price":
                        temp = temp.OrderBy(p => p.Price);
                        break;
                    case "Organizer":
                        temp = temp.OrderBy(p => p.Organizer);
                        break;
                    default:
                        temp = temp.OrderBy(p => p.EventTime);
                        break;
                }
            }
            else
            {
                switch (orderByWhat)
                {
                    case null:
                        temp = temp.OrderByDescending(p => p.EventTime);
                        break;
                    case "TicketName":
                        temp = temp.OrderByDescending(p => p.TicketName);
                        break;
                    case "Price":
                        temp = temp.OrderByDescending(p => p.Price);
                        break;
                    case "Organizer":
                        temp = temp.OrderByDescending(p => p.Organizer);
                        break;
                    default:
                        temp = temp.OrderByDescending(p => p.EventTime);
                        break;
                }
            }
            return temp.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }

}
