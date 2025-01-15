using System.ComponentModel.DataAnnotations;
using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.ViewModels.User
{
    public class UserDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [Display(Name = "Registered on")]
        public DateTime RegisteredOn { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
