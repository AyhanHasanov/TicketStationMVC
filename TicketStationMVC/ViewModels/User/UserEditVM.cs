using System.ComponentModel.DataAnnotations;
using TicketStationMVC.CustomAttributes;
using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.ViewModels.User
{
    public class UserEditVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "*this field is required!")]
        [Display(Name = "Your full name")]
        [MinLength(2, ErrorMessage = "Name must be longer!")]
        [MaxLength(255, ErrorMessage = "Name must be shorter!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*this field is required!")]
        [Display(Name = "Your email")]
        [EmailAddress(ErrorMessage = "Email is not valid!")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [PasswordComplexity(ErrorMessage = "Password does not meet the requirments!")]
        public string? Password { get; set; } = ".";

        [Required(ErrorMessage = "*this field is required!")]
        [Display(Name = "Username")]
        [MinLength(2, ErrorMessage = "Username must be longer")]
        [MaxLength(255, ErrorMessage = "Username must be shorter")]
        [Username]
        public string Username { get; set; }
        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
