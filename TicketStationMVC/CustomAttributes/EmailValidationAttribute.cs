using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TicketStationMVC.CustomAttributes
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return new ValidationResult("Email is required!");
            }

            if (!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                return new ValidationResult("Email must be correct!");
            }

            return ValidationResult.Success;
        }
    }
}

