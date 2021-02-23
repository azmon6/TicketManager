using System;
using System.Collections.Generic;
using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Abstract
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
        //Bool because we can try to add an existing User
        bool AddUser(User tempUser, LoginInformation loginInformation);
        bool IsValidUser(string Username, string Password);
        User DeleteUser(int UserId);
        void ModifyUser(User tempUser);
        User GetUser(int userID);
        User GetUser(string tempUsername);
        IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(string tempUsername);
        IEnumerable<Tuple<Transaction, Ticket>> GetUserTransactions(int userID);
    }
}
