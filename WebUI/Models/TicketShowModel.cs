using System.Collections.Generic;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class TicketShowModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public int TotalPages { get; set; }
        public int PageNow { get; set; }
    }
}