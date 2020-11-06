using TicketManager.Domain.Entities;
using System.Collections.Generic;

namespace TicketManager.Domain.Abstract
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> Tickets { get; }
        void SaveTicket(Ticket tick);
        Ticket DeleteTicket(int tickId);
    }
}
