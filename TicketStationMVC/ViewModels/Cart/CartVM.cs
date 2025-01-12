using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.ViewModels.Cart
{
    public class CartVM
    {
        public int Id { get; set; }
        public List<CartItemVM>? Items { get; set; }
        public int? OwnerId { get; set; }
        public Data.Entities.User? Owner { get; set; }
    }

}
