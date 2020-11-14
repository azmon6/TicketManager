using System.ComponentModel.DataAnnotations;
using System;

namespace TicketManager.WebUI.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class UsernameUniqueValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            // TODO See if CustomValidationAttribute will wield better code
            // for resolving unique usernames
            return false;
        }
    }
}