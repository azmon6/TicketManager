using TicketManager.Domain.Entities;
using System.Linq;

namespace TicketManager.Domain.Abstract
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Tickets { get; }

        bool SaveTicket(Ticket ticketToSave);
        Ticket DeleteTicket(int ticketToDeleteID);
        int GetSize();
        IQueryable<Ticket> GetSpecificPage(int pageSize, int pageNumber, string orderByWhat = "EventTime", string asc = "False");
    }
}
