using System;
using System.Collections.Generic;
using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Abstract
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }

        bool AddUser(User newCustomer, LoginInformation newLoginInformation);
        bool IsValidUser(string username, string password);

        void ModifyUser(User customerToModify);

        User DeleteUser(int customerID);
        User GetUser(int customerID);
        User GetUser(string customerUsername);
        
        IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(string customerUsername);
        IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(int customerID);
    }
}
