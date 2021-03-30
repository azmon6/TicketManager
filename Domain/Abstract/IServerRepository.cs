using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Abstract
{
    public interface IServerRepository
    {
        IQueryable<Transaction> Transactions { get; }
    }
}
