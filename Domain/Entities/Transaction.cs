using System;

namespace TicketManager.Domain.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }
        
        public DateTime DateMade { get; set; }
        public double PricePaid { get; set; }
        public string DealID { get; set; }
    }
}
