namespace TicketStationMVC.ViewModels.Cart
{
    public class CartItemVM
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? AddedToCart { get; set; }
    }
}
