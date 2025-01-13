using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.Data.Entities
{
    public class BaseEntity
    {
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }
        [Display(Name ="Modified at")]
        public DateTime ModifiedAt { get; set; }
        
    }
}
