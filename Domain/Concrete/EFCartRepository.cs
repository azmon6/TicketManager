﻿using System.Linq;
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

        public void CheckoutUser(User tempUser)
        {
            CheckoutUser(tempUser.UserID);
        }

        public void CheckoutUser(int userId)
        {
            Guid testGuid = Guid.NewGuid();
            string tempTime = DateTime.Now.ToString("dd/MM/yyyy");
            IQueryable<UserCartInformation> temp = context.CartInformation.Where(m => m.UserID == userId);
            foreach(var line in temp)
            {
                Transaction tempTrans = new Transaction()
                {
                    TicketID = line.TicketID,
                    DateMade = tempTime,
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
            var temp = context.CartInformation.Remove(
                context.CartInformation.First(x => x.UserID == userId && x.TicketID == tickId));
            context.SaveChanges();
            return temp;
        }

        public UserCartInformation RemoveItemFromCart(User tempUser, Ticket tempTick)
        {
            return RemoveItemFromCart(tempUser.UserID, tempTick.TicketID);
        }
    }
}
