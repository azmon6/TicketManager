using System.ComponentModel.DataAnnotations;

namespace TicketManager.WebUI.Models
{
    public class LoginInfo
    {
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}