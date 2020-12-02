using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TicketManager.Domain.Entities
{
    public class UserCartInformation
    {

        [Key]
        public int BoughtID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int TicketID { get; set; }
        public Ticket Ticket { get; set; }

        public int Quantity { get; set; }

        public string DateAdded { get; set; }
    }
}
