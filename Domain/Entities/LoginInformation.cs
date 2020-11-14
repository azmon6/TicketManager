
using System.ComponentModel.DataAnnotations;

namespace TicketManager.Domain.Entities
{
    public class LoginInformation
    {
        [Key]
        public int LoginID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
