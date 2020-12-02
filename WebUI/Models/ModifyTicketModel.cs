using System.ComponentModel.DataAnnotations;
using TicketManager.Domain.Entities;
using System.Web.Mvc;

namespace TicketManager.WebUI.Models
{
    public class ModifyTicketModel
    {

        public ModifyTicketModel()
        {

        }

        public ModifyTicketModel(Ticket tempTick)
        {
            if (tempTick == null)
                return;
            this.TicketID = tempTick.TicketID;
            this.TicketName = tempTick.TicketName;
            this.Description = tempTick.Description;
            this.Organizer = tempTick.Organizer;
            this.Price = tempTick.Price;
            this.StartBuyTime = tempTick.StartBuyTime;
            this.EndBuyTime = tempTick.EndBuyTime;
            this.EventTime = tempTick.EventTime;
        }

        public Ticket GetTicket()
        {
            Ticket temp = new Ticket();
            temp.TicketID = this.TicketID;
            temp.TicketName = this.TicketName;
            temp.Description = this.Description;
            temp.Organizer = this.Organizer;
            temp.Price = this.Price;
            temp.StartBuyTime = this.StartBuyTime;
            temp.EndBuyTime = this.EndBuyTime;
            temp.EventTime = this.EventTime;
            return temp;
        }

        [HiddenInput(DisplayValue = false)]
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Enter a Ticket name")]
        [MinLength(5, ErrorMessage = "Must be at least 5 characters")]
        public string TicketName { get; set; }

        [Required(ErrorMessage = "Enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter a Organizer")]
        public string Organizer { get; set; }

        [Required(ErrorMessage = "Enter a Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be a valid price")]
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