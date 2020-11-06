using System.Collections.Generic;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class TicketShowModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}