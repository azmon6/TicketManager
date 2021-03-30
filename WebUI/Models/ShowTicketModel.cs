using TicketManager.Domain.Entities;
using System.Collections.Generic;

namespace TicketManager.WebUI.Models
{
    public class ShowTicketModel
    {
        public Ticket TicketToShow { get; set; }
        public IEnumerable<Ticket> OtherTicketsByOrg { get; set; }
    }
}