using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.CustomAttributes
{
    public class UsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var username = value as string;

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
                return new ValidationResult("Username must not be empty!");

            if (char.IsDigit(username[0]))
                return new ValidationResult("Username must not start with a digit");

            if (username.Split(' ').Count() > 1)
                return new ValidationResult("Username must not contain any spaces.");

            return ValidationResult.Success;
        }
    }
}
