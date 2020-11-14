using TicketManager.Domain.Entities;
using System.Linq;

namespace TicketManager.Domain.Abstract
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Tickets { get; }
        void SaveTicket(Ticket tick);
        Ticket DeleteTicket(int tickId);
    }
}
