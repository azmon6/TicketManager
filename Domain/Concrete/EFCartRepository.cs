using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace TicketManager.Domain.Concrete
{
    
    public class EFCartRepository : ICartRepository
    {
        private EntityContext context = new EntityContext();

        public IQueryable<ShoppingCarts> CartInfo
        {
            get
            {
                return context.CartInformation;
            }
        }

        public IQueryable<User> UserInfo
        {
            get
            {
                return context.Users;
            }
        }

        public IQueryable<Ticket> TicketInfo
        {
            get
            {
                return context.Tickets;
            }
        }

        public void AddTicketToUser(User customer, Ticket ticketToBuy, int quantity = 1)
        {
            AddTicketToUser(customer.UserID, ticketToBuy.TicketID, quantity);
        }

        public void AddTicketToUser(int customerID, int ticketToBuyID, int quantity = 1)
        {
            if (quantity < 1) { return; }

            ShoppingCarts customerCartLine = CartInfo.FirstOrDefault(
                p => (p.UserID == customerID && p.TicketID == ticketToBuyID));
            if (customerCartLine != null)
            {
                if(customerCartLine.CheckOutTime != null)
                {
                    TicketInfo.First(x => x.TicketID == ticketToBuyID).AmountRemaining += customerCartLine.Quantity;
                }
                customerCartLine.Quantity += quantity;
                customerCartLine.CheckOutTime = null;
            }
            else
            {
                customerCartLine = new ShoppingCarts();
                customerCartLine.Quantity = quantity;
                customerCartLine.Ticket = context.Tickets.Find(ticketToBuyID);
                customerCartLine.User = context.Users.Find(customerID);
                customerCartLine.DateAdded = DateTime.UtcNow;
                context.CartInformation.Add(customerCartLine);
            }
            context.SaveChanges();
        }

        public void CheckoutUser(User customer)
        {
            CheckoutUser(customer.UserID);
        }

        public void CheckoutUser(int customerID)
        {
            Guid TransactionID = Guid.NewGuid();
            
            IQueryable<ShoppingCarts> customerCart = CartInfo.Where(m => m.UserID == customerID);
            
            foreach(var line in customerCart)
            {

                Transaction tempTrans = new Transaction()
                {
                    TicketID = line.TicketID,
                    DateMade = DateTime.UtcNow,
                    DealID = TransactionID.ToString(),
                    UserID = line.UserID,
                    PricePaid = TicketInfo.First(x => x.TicketID == line.TicketID).Price * line.Quantity
                };
                context.Transactions.Add(tempTrans);
            }
            context.SaveChanges();
            RemoveAllFromCart(customerID);
        }

        public IQueryable<ShoppingCarts> RemoveAllFromCart(int customerID)
        {

            var temp = context.CartInformation.RemoveRange(CartInfo.Where(x => x.UserID == customerID)).AsQueryable();
            context.SaveChanges();
            return temp;
        }

        public IQueryable<ShoppingCarts> RemoveAllFromCart(User customer)
        {
            return RemoveAllFromCart(customer.UserID);
        }

        public ShoppingCarts RemoveItemFromCart(int customerID, int ticketToRemoveID)
        {
            var CartLineToRemove = context.CartInformation.First(x => x.UserID == customerID && x.TicketID == ticketToRemoveID);
            context.CartInformation.Remove(CartLineToRemove);
            if(CartLineToRemove.CheckOutTime != null)
            {
                context.Tickets.Find(ticketToRemoveID).AmountRemaining += CartLineToRemove.Quantity;
            }
            context.SaveChanges();
            return CartLineToRemove;
        }

        public ShoppingCarts RemoveItemFromCart(User customer, Ticket ticketToRemove)
        {   
            return RemoveItemFromCart(customer.UserID, ticketToRemove.TicketID);
        }


        public string SubtractTicket(ShoppingCarts customerCartLine)
        {
            if(customerCartLine.CheckOutTime != null)
            {
                return "";
            }

            if(customerCartLine.Ticket.AmountRemaining < customerCartLine.Quantity)
            {
                return customerCartLine.Ticket.TicketName;
            }

            customerCartLine.Ticket.AmountRemaining -= customerCartLine.Quantity;
            customerCartLine.CheckOutTime = DateTime.UtcNow;

            return "";
        }

        private List<string> TryReserveTickets(int customerID)
        {
            List<string> result = new List<string>();

            IEnumerable<ShoppingCarts> customerCart = CartInfo.Where(x =>
                x.UserID == customerID).ToList();

            string returnedMessage = "";
            foreach (var tempLine in customerCart)
            {
                returnedMessage = SubtractTicket(tempLine);
                if (returnedMessage != "")
                {
                    result.Add(returnedMessage);
                }
            }
            context.SaveChanges();
            return result;
        }

        public List<string> ReserveTickets(int customerID)
        {
            for(int i = 0; i<=5;i++)
            {
                //TODO ASK IF THIS BLOWS UP THE WORLD
                context = new EntityContext();
                try
                {
                    return TryReserveTickets(customerID);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {

                        var databaseValues = entry.GetDatabaseValues();

                        entry.CurrentValues.SetValues(databaseValues);
                        entry.OriginalValues.SetValues(databaseValues);
                        
                    }
                }
            }

            return new List<string> { "NotPossible" };
        }

        public void ClearCart(int customerID)
        {
            var customerCartToClear = CartInfo.Where(x => x.UserID == customerID);
            foreach(var i in customerCartToClear)
            {
                if(i.CheckOutTime != null)
                {
                    i.Ticket.AmountRemaining += i.Quantity;
                }
            }
            context.CartInformation.RemoveRange(CartInfo.Where(x => x.UserID == customerID));
            context.SaveChanges();
        }

        public void RefreshOldCarts()
        {
            var timeRefreshed = DateTime.UtcNow;
            foreach (var i in CartInfo)
            {
                if (i.CheckOutTime != null)
                {
                    var tempI = i.CheckOutTime.Value;
                    if ((timeRefreshed - tempI).TotalMinutes >= 1)
                    {
                        i.Ticket.AmountRemaining += i.Quantity;
                        i.CheckOutTime = null;
                    }
                }
            }
            context.SaveChanges();
        }

        public bool IsUserAllTicketsReserved(int customerID)
        {
            IEnumerable<ShoppingCarts> tempInfo = CartInfo.Where(x =>
                   x.UserID == customerID && x.CheckOutTime==null).ToList();
            return !(tempInfo.Count() == 0);
        }

        public IQueryable<ShoppingCarts> GetUserCart(int customerID)
        {
            return CartInfo.Where(x => x.UserID == customerID);
        }

    }
}
