using System.ComponentModel.DataAnnotations;
using TicketManager.WebUI.Infrastructure;
using TicketManager.Domain.Entities;

namespace TicketManager.WebUI.Models
{
    public class SignUpInformation
    {
        [Required(ErrorMessage ="Enter Username")]
        //[RegularExpression("(?!.*[^a-zA-Z0-9])", ErrorMessage ="Username mustn't contain special characters")]
        //[UsernameUniqueValidation(ErrorMessage ="This username exists already")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6,ErrorMessage ="Min lenght is 6 for password.")]
        [RegularExpression(".*[A-Z].*", ErrorMessage ="At least one UpperCase character in password")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Confirm password is not the same")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        //[RegularExpression("(?!.*[^a-zA-Z0-9])", ErrorMessage = "Name mustn't contain special characters")]
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