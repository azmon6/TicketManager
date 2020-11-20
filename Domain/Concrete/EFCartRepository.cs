using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System;

namespace TicketManager.Domain.Concrete
{
    public class EFCartRepository : ICartRepository
    {
        EntityContext context = new EntityContext();

        public IQueryable<UserCartInformation> CartInformation
        {
            get
            {
                return context.CartInformation;
            }
        }

        public void AddTicketToUser(User tempUser, Ticket tempTick, int quantity = 1)
        {
            if(quantity < 1) { return; }

            UserCartInformation tempInfo = CartInformation.FirstOrDefault(
                p => (p.UserID == tempUser.UserID && p.TicketID == tempTick.TicketID));
            if(tempInfo != null)
            {
                tempInfo.Quantity += quantity;
            }
            else
            {
                //ASK why tempInfo.UserID = tempUser makes new User;
                tempInfo = new UserCartInformation();
                tempInfo.Quantity = quantity;
                tempInfo.Ticket = tempTick;
                tempInfo.User = tempUser;
                tempInfo.DateAdded = DateTime.Now.Date.ToString("dd/MM/yyyy");
                context.CartInformation.Add(tempInfo);
            }
            context.SaveChanges();
        }

        public void AddTicketToUser(int userId, int tickId, int quantity = 1)
        {
            if (quantity < 1) { return; }

            UserCartInformation tempInfo = CartInformation.FirstOrDefault(
                p => (p.UserID == userId && p.TicketID == tickId));
            if (tempInfo != null)
            {
                tempInfo.Quantity += quantity;
            }
            else
            {
                tempInfo = new UserCartInformation();
                tempInfo.Quantity = quantity;
                tempInfo.Ticket = context.Tickets.Find(tickId);
                tempInfo.User = context.Users.Find(userId);
                tempInfo.DateAdded = DateTime.Now.Date.ToString("dd/MM/yyyy");
                context.CartInformation.Add(tempInfo);
            }
            context.SaveChanges();
        }

        public IQueryable<UserCartInformation> RemoveAllFromCart(int userId, int tickId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<UserCartInformation> RemoveAllFromCart(User tempUser, Ticket tempTick)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<UserCartInformation> RemoveItemFromCart(int userId, int tickId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<UserCartInformation> RemoveItemFromCart(User tempUser, Ticket tempTick)
        {
            throw new System.NotImplementedException();
        }
    }
}
