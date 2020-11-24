using System.ComponentModel.DataAnnotations;

namespace TicketManager.WebUI.Infrastructure.Attributes
{
    public class CreditCardValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string number = value as string;
            int digitsNumber = number.Length;
            int sum = 0;
            bool second = false;
            for( int i = digitsNumber - 1; i>=0; i--)
            {
                int d = number[i] - '0';
                if (second == true)
                    d = d * 2;
                sum += d / 10;
                sum += d % 10;
                second = !second;
            }
            return (sum % 10 == 0);
        }
    }
}