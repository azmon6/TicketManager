using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TicketManager.Domain.Entities
{

    public class Ticket
    {
        //TODO ADD required but not with errormessage
        [HiddenInput(DisplayValue = false)]
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Enter a Ticket name")]
        [MinLength(5,ErrorMessage ="Must be at least 5 characters")]
        public string TicketName { get; set; }

        [Required(ErrorMessage = "Enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter a Organizer")]
        public string Organizer { get; set; }
        
        [Required(ErrorMessage = "Enter a Price")]
        [Range(0.01,double.MaxValue,ErrorMessage = "Value must be a valid price")]
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
