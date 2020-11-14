using System.ComponentModel.DataAnnotations;
using TicketManager.WebUI.Infrastructure;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class SignUpInformation
    {
        [Required(ErrorMessage ="Enter Username")]
        //[UsernameUniqueValidation(ErrorMessage ="This username exists already")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }


        public User getUser()
        {
            return new User { Name = this.Name };
        }

        public LoginInformation GetLogin()
        {
            return new LoginInformation { Username = this.Username, Password = this.Password };
        }
    }
}