using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.Data.Entities
{
    public class CartItem : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
