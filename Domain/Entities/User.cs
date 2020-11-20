using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TicketManager.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }

        public int LoginID { get; set; }
        public LoginInformation LoginInformation { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }
    }
}
