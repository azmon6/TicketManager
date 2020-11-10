using System.ComponentModel.DataAnnotations;

namespace TicketManager.Domain.Entities
{
    public class Ticket
    {
        //TODO ADD required but not with errormessage
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Enter a Ticket name")]
        public string TicketName { get; set; }

        [Required(ErrorMessage = "Enter a Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter a Organizer")]
        public string Organizer { get; set; }
        [Required(ErrorMessage = "Enter a Price")]
        public double Price { get; set; }

        //TODO Make Dates from string to DateTime
        //Use Jquerry for date picker
        [Required(ErrorMessage = "Enter a Start of buying date")]
        public string StartBuyTime { get; set; }
        [Required(ErrorMessage = "Enter a End of bying date")]
        public string EndBuyTime { get; set; }
        [Required(ErrorMessage = "Enter a Event start time")]
        public string EventTime { get; set; }
    }
}
