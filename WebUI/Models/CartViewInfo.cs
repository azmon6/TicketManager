using System.Linq;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class CartViewInfo
    {
        public string TicketName { get; set; }
        public string EventTime { get; set; }
        public int Quantity { get; set; }
        public double TicketPrice { get; set; }
    }
}