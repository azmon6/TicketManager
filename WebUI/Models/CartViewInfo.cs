using System.Linq;
using TicketManager.Domain.Entities;
using System;

namespace TicketManager.WebUI.Models
{
    public class CartViewInfo
    {
        public int TicketID { get; set; }
        public string TicketName { get; set; }
        public DateTime EventTime { get; set; }
        public int Quantity { get; set; }
        public double TicketPrice { get; set; }
    }
}