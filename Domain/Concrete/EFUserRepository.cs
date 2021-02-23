using System;
using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace TicketManager.Domain.Concrete
{
    public class EFUserRepository : IUsersRepository
    {
        private EntityContext context = new EntityContext();

        public IQueryable<User> Users
        {
            get
            {
                return context.Users;
            }
        }

        public bool AddUser(User tempUser, LoginInformation loginInformation)
        {

            var temp = context.LoginInformations.Where(x => x.Username == loginInformation.Username).ToList();

            if(tempUser.UserID == 0 && loginInformation.LoginID == 0 && temp.Count == 0)
            {
                loginInformation.Password = GetMD5(loginInformation.Password);
                tempUser.LoginInformation = context.LoginInformations.Add(loginInformation);
                tempUser.Role = "Normal";
                context.Users.Add(tempUser);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool IsValidUser(string Username, string Password)
        {
            string temp = GetMD5(Password);
            bool isValid = context.LoginInformations.Any(p => p.Username == Username &&
                p.Password == temp);

            return isValid;
        }

        public void ModifyUser(User tempUser)
        {
            User foundUser = context.Users.FirstOrDefault(p => p.UserID == tempUser.UserID);
            if(foundUser == null)
            {
                return;
            }

            foundUser.Name = tempUser.Name;
            foundUser.Role = tempUser.Role;
            context.SaveChanges();
        }

        public User GetUser(int userID)
        {
            return Users.FirstOrDefault(p => p.UserID == userID);
        }

        public User GetUser(string tempUsername)
        {
            return Users.FirstOrDefault(p => p.LoginInformation.Username == tempUsername);
        }

        public User DeleteUser(int UserId)
        {
            //TODO DELETE USERS
            throw new NotImplementedException();
            //return new User();
        } 

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(string tempUsername)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(int userID)
        {
            
            var temp = context.Transactions.Where(x => x.UserID == userID).Join(context.Tickets, tran => tran.TicketID, tick => tick.TicketID,
                (tran, tick) => new { Trtemp = tran, TickTemp = tick }
                ).ToList().Select(x => new Tuple<Transaction, Ticket> (x.Trtemp,x.TickTemp) );
            
            //TODO WHY LINQ WHY
            /*
            var temp = (from trans in context.Transactions
                       join tick in context.Tickets on trans.TicketID equals tick.TicketID
                       select new { Trtemp = trans, TickTemp = tick }).ToList().Select(
                    x => new Transaction { TransactionID = x.Trtemp.TransactionID}
                );
            */
            return temp;
        }
    }
}
