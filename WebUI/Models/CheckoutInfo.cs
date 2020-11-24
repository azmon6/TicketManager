using System.ComponentModel.DataAnnotations;
using TicketManager.WebUI.Infrastructure.Attributes;

namespace TicketManager.WebUI.Models
{
    public class CheckoutInfo
    {
        [Required(ErrorMessage ="Enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        [Display(Name = "Address line one")]
        public string AddressLineOne { get; set; }

        [Display(Name = "Address line two")]
        public string AddressLineTwo { get; set; }

        [Required(ErrorMessage = "Enter your city.")]
        public string City;

        [Required(ErrorMessage = "Enter credit card number")]
        [RegularExpression("^[0-9]{14,19}$",ErrorMessage = "Enter valid credit card number.")]
        [CreditCardValidation(ErrorMessage ="Credit card is not valid.")]
        [Display(Name ="Credit card details.")]
        public string CreditCardDetails { get; set; }
    }
}