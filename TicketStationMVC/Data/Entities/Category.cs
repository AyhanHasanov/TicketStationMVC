using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.Data.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        [MinLength(2)]
        public string Name { get; set; }
        public virtual ICollection<EventCategories> EventCategories { get; set; }
    }
}
