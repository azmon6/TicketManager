
namespace TicketManager.Domain.Entities
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int TicketID { get; set; }
        public Ticket Ticket { get; set; }
        public string DateMade { get; set; }
        public double PricePaid { get; set; }
    }
}
