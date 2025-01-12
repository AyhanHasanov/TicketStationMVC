using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.Data.Entities
{
    public class Event : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(5000)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }

        public User? CreatedBy { get; set; }

        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }

        [Required]
        public bool Status { get; set; } // is active or passed

        public string ImageURL { get; set; }

        public virtual ICollection<EventCategories> EventCategories { get; set;}
    }
}
