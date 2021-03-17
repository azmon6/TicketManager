using System.Linq;
using TicketManager.Domain.Entities;
using System.Collections.Generic;

namespace TicketManager.Domain.Abstract
{
    public interface ICartRepository
    {
        IQueryable<ShoppingCarts> CartInfo { get; }
        IQueryable<User> UserInfo { get; }
        IQueryable<Ticket> TicketInfo { get; }

        void AddTicketToUser(User customer, Ticket ticketToBuy, int quantity = 1);
        void AddTicketToUser(int customerID, int ticketToBuyID, int quantity = 1);
        
        void CheckoutUser(User customer);
        void CheckoutUser(int customerID);
        
        void ClearCart(int customerID);
        void RefreshOldCarts();

        ShoppingCarts RemoveItemFromCart(User customer, Ticket ticketToRemove);
        ShoppingCarts RemoveItemFromCart(int customerID,int ticketToRemoveID);

        IQueryable<ShoppingCarts> RemoveAllFromCart(User customer);
        IQueryable<ShoppingCarts> RemoveAllFromCart(int customerID);
        
        bool PreCheckoutCheck(int customerID);
        bool IsUserAllTicketsReserved(int customerID);


        string SubtractTicket(ShoppingCarts customerCartLine);
        List<string> ReserveTickets(int customerID);
    }
}
