
namespace TicketManager.Domain.Entities
{

    public class Ticket
    {
        public int TicketID { get; set; }

        public string TicketName { get; set; }

        public string Description { get; set; }

        public string Organizer { get; set; }
        
        public double Price { get; set; }

        //TODO Make Dates from string to DateTime
        //Use Jquerry for date picker
        public string StartBuyTime { get; set; }
        
        public string EndBuyTime { get; set; }

        public string EventTime { get; set; }
    }
}
