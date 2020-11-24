using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Abstract
{
    public interface ICartRepository
    {
        IQueryable<UserCartInformation> CartInformation { get; }
        IQueryable<User> UserInfo { get; }
        IQueryable<Ticket> TicketInfo { get; }
        void AddTicketToUser(User tempUser, Ticket tempTick, int quantity = 1);
        void AddTicketToUser(int userId, int tickId, int quantity = 1);
        UserCartInformation RemoveItemFromCart(int userId,int tickId);
        UserCartInformation RemoveItemFromCart(User tempUser, Ticket tempTick);
        IQueryable<UserCartInformation> RemoveAllFromCart(int userId);
        IQueryable<UserCartInformation> RemoveAllFromCart(User tempUser);
        void CheckoutUser(User tempUser);
        void CheckoutUser(int userId);
    }
}
