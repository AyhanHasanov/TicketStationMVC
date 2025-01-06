using System.ComponentModel.DataAnnotations;
using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.ViewModels.Events
{
    public class EventViewVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public DateTime DateOfEvent { get; set; }

        public string ImageURL { get; set; }

        public virtual ICollection<string> Categories { get; set; }
    }
}
