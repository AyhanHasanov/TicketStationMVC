using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketStationMVC.CustomAttributes;
using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.ViewModels.Events
{
    public class EventCreateVM
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
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }

        [Required]
        public bool Status { get; set; } // is active or passed

        public string? ImageURL { get; set; }

        [DisplayName("Categories")]
        [EnsureOneElement(ErrorMessage = "Please select at least one genre!")]
        public List<int>? CategoryIds { get; set; }


        public int CreatedById { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile? ImageFile { get; set; }


    }
}
