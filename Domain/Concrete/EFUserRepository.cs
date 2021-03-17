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

        public bool AddUser(User newCustomer, LoginInformation newLoginInformation)
        {

            var usersWithUsername = context.LoginInformations.Where(x => x.Username == newLoginInformation.Username).ToList();

            if(newCustomer.UserID == 0 && newLoginInformation.LoginID == 0 && usersWithUsername.Count == 0)
            {
                newLoginInformation.Password = GetMD5(newLoginInformation.Password);
                newCustomer.LoginInformation = context.LoginInformations.Add(newLoginInformation);
                newCustomer.Role = "Normal";
                context.Users.Add(newCustomer);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool IsValidUser(string username, string password)
        {
            string temp = GetMD5(password);
            bool isValid = context.LoginInformations.Any(p => p.Username == username &&
                p.Password == temp);

            return isValid;
        }

        public void ModifyUser(User customerToModify)
        {
            User foundUser = context.Users.FirstOrDefault(p => p.UserID == customerToModify.UserID);
            if(foundUser == null)
            {
                return;
            }

            foundUser.Name = customerToModify.Name;
            foundUser.Role = customerToModify.Role;
            context.SaveChanges();
        }

        public User GetUser(int customerID)
        {
            return Users.FirstOrDefault(p => p.UserID == customerID);
        }

        public User GetUser(string customerUsername)
        {
            return Users.FirstOrDefault(p => p.LoginInformation.Username == customerUsername);
        }

        public User DeleteUser(int customerID)
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

        public IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(string customerUsername)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(int customerID)
        {
            
            return context.Transactions.Where(x => x.UserID == customerID).Join(context.Tickets, tran => tran.TicketID, tick => tick.TicketID,
                (tran, tick) => new { Trtemp = tran, TickTemp = tick }
                ).ToList().Select(x => new Tuple<Transaction, Ticket>(x.Trtemp, x.TickTemp));
        }
    }
}
