using System;
using System.Collections.Generic;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class ProfileInfo
    {
        public string DisplayName { get; set; }
        public string[] Roles { get; set; }
        public IEnumerable<Tuple<Transaction, Ticket>> UserTransactions { get; set; }
    }
}