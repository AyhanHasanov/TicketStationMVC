using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.ViewModels.User
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "*this field is required!")]
        [Display(Name = "Username or Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*this field is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
