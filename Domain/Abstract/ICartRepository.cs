using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Abstract
{
    public interface ICartRepository
    {
        IQueryable<UserCartInformation> CartInformation { get; }
        void AddTicketToUser(User tempUser, Ticket tempTick, int quantity = 1);
        void AddTicketToUser(int userId, int tickId, int quantity = 1);
        IQueryable<UserCartInformation> RemoveItemFromCart(int userId,int tickId);
        IQueryable<UserCartInformation> RemoveItemFromCart(User tempUser, Ticket tempTick);
        IQueryable<UserCartInformation> RemoveAllFromCart(int userId, int tickId);
        IQueryable<UserCartInformation> RemoveAllFromCart(User tempUser, Ticket tempTick);
    }
}
