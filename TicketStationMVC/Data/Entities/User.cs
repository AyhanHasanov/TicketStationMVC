using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TicketStationMVC.Data.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        public DateTime RegisteredOn { get; set; }
        //public virtual ICollection<UserRole>? UserRoles { get; set; }
        public Role? Role { get; set; }
        public int? RoleId { get; set; }

    }
}
