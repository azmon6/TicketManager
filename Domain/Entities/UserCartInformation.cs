using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace TicketManager.Domain.Entities
{
    public class UserCartInformation
    {

        [Key]
        public int BoughtID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }

        public int Quantity { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? CheckOutTime { get; set; }
    }
}
