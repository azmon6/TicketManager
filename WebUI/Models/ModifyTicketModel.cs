using System.ComponentModel.DataAnnotations;
using TicketManager.Domain.Entities;
using System.Web.Mvc;
using System;

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
            {
                this.StartingDateAvailable = DateTime.UtcNow;
                this.EndDateAvailable = DateTime.UtcNow;
                this.TimeOfEvent = DateTime.UtcNow;
                return;
            }
            this.TicketID = tempTick.TicketID;
            this.TicketName = tempTick.TicketName;
            this.Description = tempTick.Description;
            this.Organizer = tempTick.Organizer;
            this.Price = tempTick.Price;
            this.StartingDateAvailable = tempTick.StartingDateAvailable;
            this.EndDateAvailable = tempTick.EndDateAvailable;
            this.TimeOfEvent = tempTick.TimeOfEvent;
            this.AmountRemaining = tempTick.AmountRemaining;
            this.RowVersion = tempTick.RowVersion;
        }

        public Ticket GetTicket()
        {
            Ticket temp = new Ticket();
            temp.TicketID = this.TicketID;
            temp.TicketName = this.TicketName;
            temp.Description = this.Description;
            temp.Organizer = this.Organizer;
            temp.Price = this.Price;
            temp.StartingDateAvailable = this.StartingDateAvailable;
            temp.EndDateAvailable = this.EndDateAvailable;
            temp.TimeOfEvent = this.TimeOfEvent;
            temp.AmountRemaining = this.AmountRemaining;
            temp.RowVersion = this.RowVersion;
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

        [Required(ErrorMessage = "Enter how many tickets available.")]
        public int AmountRemaining { get; set; }

        [Required(ErrorMessage = "Enter a Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be a valid price")]
        public double Price { get; set; }

        //TODO Make Dates from string to DateTime
        //Use Jquerry for date picker
        [Required(ErrorMessage = "Enter a Start of buying date")]
        public DateTime StartingDateAvailable { get; set; }

        [Required(ErrorMessage = "Enter a End of bying date")]
        public DateTime EndDateAvailable { get; set; }

        [Required(ErrorMessage = "Enter a Event start time")]
        public DateTime TimeOfEvent { get; set; }

        public byte[] RowVersion { get; set; }
    }
}