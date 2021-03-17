using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.Domain.Entities
{

    public class Ticket
    {
        //TO DO RENAME FIELD/TABLES

        public int TicketID { get; set; }

        public string TicketName { get; set; }

        public string Description { get; set; }

        public string Organizer { get; set; }
        
        public double Price { get; set; }

        //TODO Make Dates from string to DateTime
        //Use Jquerry for date picker
        public DateTime StartingDateAvailable { get; set; }
        
        public DateTime EndDateAvailable { get; set; }

        public DateTime TimeOfEvent { get; set; }

        public int AmountRemaining { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
