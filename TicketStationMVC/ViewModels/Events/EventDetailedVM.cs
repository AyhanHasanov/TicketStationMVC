using System.ComponentModel.DataAnnotations;
using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.ViewModels.Events
{
    public class EventDetailedVM
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string? CreatedByUsername { get; set; }

        public bool Status { get; set; } // is active or passed

        public string ImageURL { get; set; }

        public virtual ICollection<string> Categories { get; set; }
    }
}
