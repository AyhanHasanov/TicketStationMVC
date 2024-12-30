using System.ComponentModel.DataAnnotations;

namespace TicketStationMVC.Data.Entities
{
    public class Cart : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
    }
}
