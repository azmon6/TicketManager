using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System.Linq;

namespace TicketManager.Domain.Concrete
{
    public class EFServerRepository : IServerRepository
    {
        private EntityContext context = new EntityContext();

        public IQueryable<Transaction> Transactions
        {
            get
            {
                return context.Transactions;
            }
        }
    }
}
