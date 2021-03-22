using System.Collections.Generic;
using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class ShowCartInfo
    {
        public IEnumerable<CartViewInfo> userCartItems { get; set; }
        public IEnumerable<string> returnedUnavailableItems { get; set; }

        public ShowCartInfo() { }
        public ShowCartInfo(IQueryable<ShoppingCarts> userCart , IQueryable<Ticket> tickets)
        {
            IEnumerable<CartViewInfo> cartItems = userCart.
                Join(tickets,
                p => p.TicketID, c => c.TicketID, (p, c) =>
                new CartViewInfo()
                {
                    TicketID = p.TicketID,
                    EventTime = c.TimeOfEvent,
                    Quantity = p.Quantity,
                    TicketPrice = c.Price,
                    TicketName = c.TicketName
                }).ToList();

            userCartItems = cartItems;
        }
    }
}