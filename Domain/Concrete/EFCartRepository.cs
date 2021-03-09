using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System;

namespace TicketManager.Domain.Concrete
{
    public class EFCartRepository : ICartRepository
    {
        //TODO Context in constructors
        private EntityContext context = new EntityContext();

        public IQueryable<UserCartInformation> CartInformation
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

        public void AddTicketToUser(User tempUser, Ticket tempTick, int quantity = 1)
        {
            AddTicketToUser(tempUser.UserID, tempTick.TicketID, quantity);
        }

        public void AddTicketToUser(int userId, int tickId, int quantity = 1)
        {
            if (quantity < 1) { return; }

            UserCartInformation tempInfo = CartInformation.FirstOrDefault(
                p => (p.UserID == userId && p.TicketID == tickId));
            if (tempInfo != null)
            {
                if(tempInfo.CheckOutTime != null)
                {
                    TicketInfo.First(x => x.TicketID == tickId).AmountRemaining += tempInfo.Quantity;
                }
                tempInfo.Quantity += quantity;
                tempInfo.CheckOutTime = null;
            }
            else
            {
                tempInfo = new UserCartInformation();
                tempInfo.Quantity = quantity;
                tempInfo.Ticket = context.Tickets.Find(tickId);
                tempInfo.User = context.Users.Find(userId);
                tempInfo.DateAdded = DateTime.Now;
                context.CartInformation.Add(tempInfo);
            }
            context.SaveChanges();
        }

        public void CheckoutUser(User tempUser)
        {
            CheckoutUser(tempUser.UserID);
        }

        public void CheckoutUser(int userId)
        {
            Guid testGuid = Guid.NewGuid();
            
            IQueryable<UserCartInformation> temp = CartInformation.Where(m => m.UserID == userId);
            
            foreach(var line in temp)
            {

                Transaction tempTrans = new Transaction()
                {
                    TicketID = line.TicketID,
                    DateMade = DateTime.Now,
                    DealID = testGuid.ToString(),
                    UserID = line.UserID,
                    PricePaid = TicketInfo.First(x => x.TicketID == line.TicketID).Price * line.Quantity
                };
                context.Transactions.Add(tempTrans);
            }
            context.SaveChanges();
            RemoveAllFromCart(userId);
        }

        public IQueryable<UserCartInformation> RemoveAllFromCart(int userId)
        {

            var temp = context.CartInformation.RemoveRange(CartInformation.Where(x => x.UserID == userId)).AsQueryable();
            context.SaveChanges();
            return temp;
        }

        public IQueryable<UserCartInformation> RemoveAllFromCart(User tempUser)
        {
            return RemoveAllFromCart(tempUser.UserID);
        }

        public UserCartInformation RemoveItemFromCart(int userId, int tickId)
        {

            //TO DO Lazy loading does not work
            var temp = context.CartInformation.First(x => x.UserID == userId && x.TicketID == tickId);
            var temptick = context.Tickets.Find(tickId);
            context.CartInformation.Remove(temp);
            if(temp.CheckOutTime != null)
            {
                temptick.AmountRemaining += temp.Quantity;
            }
            context.SaveChanges();
            return temp;
        }

        public UserCartInformation RemoveItemFromCart(User tempUser, Ticket tempTick)
        {   
            return RemoveItemFromCart(tempUser.UserID, tempTick.TicketID);
        }


        public string SubtractTicket(UserCartInformation templine)
        {

            //TO DO Bad practice to return the string? Make it side effect?

            if(templine.CheckOutTime != null)
            {
                return "";
            }

            if(templine.Ticket.AmountRemaining < templine.Quantity)
            {
                return templine.Ticket.TicketName;
            }

            templine.Ticket.AmountRemaining -= templine.Quantity;
            templine.CheckOutTime = DateTime.UtcNow;

            context.SaveChanges();

            return "";
        }



        public void ClearCart(int userID)
        {
            var temp = CartInformation.Where(x => x.UserID == userID);
            foreach(var i in temp)
            {
                if(i.CheckOutTime != null)
                {
                    i.Ticket.AmountRemaining += i.Quantity;
                }
            }
            context.CartInformation.RemoveRange(CartInformation.Where(x => x.UserID == userID));
            context.SaveChanges();
        }

        public void RefreshOldCarts()
        {
            var tempTime = DateTime.Now;
            foreach (var i in CartInformation)
            {
                if (i.CheckOutTime != null)
                {
                    var tempI = i.CheckOutTime.Value;
                    if ((tempTime-tempI).TotalMinutes >= 1)
                    {
                        i.Ticket.AmountRemaining += i.Quantity;
                        i.CheckOutTime = null;
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
