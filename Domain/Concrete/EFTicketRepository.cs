using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System.Data.Entity.Infrastructure;

namespace TicketManager.Domain.Concrete
{
    public class EFTicketRepository : ITicketRepository
    {
        private EntityContext context = new EntityContext();

        public IQueryable<Ticket> Tickets
        {
            get { return context.Tickets; }
        }

        public bool SaveTicket( Ticket ticketToSave)
        {
            if(ticketToSave.TicketID == 0)
            {
                context.Tickets.Add(ticketToSave);
            }
            else
            {
                Ticket Entry = context.Tickets.Find(ticketToSave.TicketID);
                if(Entry != null)
                {
                    if (!Entry.RowVersion.SequenceEqual(ticketToSave.RowVersion))
                    {
                        ticketToSave.RowVersion = Entry.RowVersion;
                        return false;
                    }

                    Entry.TicketName = ticketToSave.TicketName;
                    Entry.Description = ticketToSave.Description;
                    Entry.Organizer = ticketToSave.Organizer;
                    Entry.TimeOfEvent = ticketToSave.TimeOfEvent;
                    Entry.StartingDateAvailable = ticketToSave.StartingDateAvailable;
                    Entry.EndDateAvailable = ticketToSave.EndDateAvailable;
                    Entry.Price = ticketToSave.Price;
                    Entry.AmountRemaining = ticketToSave.AmountRemaining;
                    
                }
            }

            try
            {
                context.SaveChanges();
                return true;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    var databaseValues = entry.GetDatabaseValues();
                    entry.OriginalValues.SetValues(databaseValues);
                }
                return false;
            }
        }

        public Ticket DeleteTicket(int ticketToDeleteID)
        {
            Ticket Entry = context.Tickets.Find(ticketToDeleteID);
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
            var result = context.Tickets.AsQueryable();
            if (asc == "True")
            {
                switch (orderByWhat)
                {
                    case null:
                        result = result.OrderBy(p => p.TimeOfEvent);
                        break;
                    case "TicketName":
                        result = result.OrderBy(p => p.TicketName);
                        break;
                    case "Price":
                        result = result.OrderBy(p => p.Price);
                        break;
                    case "Organizer":
                        result = result.OrderBy(p => p.Organizer);
                        break;
                    default:
                        result = result.OrderBy(p => p.TimeOfEvent);
                        break;
                }
            }
            else
            {
                switch (orderByWhat)
                {
                    case null:
                        result = result.OrderByDescending(p => p.TimeOfEvent);
                        break;
                    case "TicketName":
                        result = result.OrderByDescending(p => p.TicketName);
                        break;
                    case "Price":
                        result = result.OrderByDescending(p => p.Price);
                        break;
                    case "Organizer":
                        result = result.OrderByDescending(p => p.Organizer);
                        break;
                    default:
                        result = result.OrderByDescending(p => p.TimeOfEvent);
                        break;
                }
            }
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }

}
