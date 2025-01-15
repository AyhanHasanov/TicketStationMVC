using Microsoft.AspNetCore.Mvc;
using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.Services.ServiceInterfaces
{
    public interface ICartService
    {
        public Task<Cart?> EnsureUserHasCartAsync(int userId);
        public Task<Cart> GetCartByUserIdAsync(int userId);
        public Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        public Task<CartItem> IncreaseQuantityOfCartItemAsync(int cartItemId);
        public Task<CartItem> DecreaseQuantityOfCartItemAsync(int cartItemId);
        public Task<CartItem> RemoveCartItemFromCartAsync(int cartItemId);
        public Task<CartItem> AddItemToCartAsync(int cartId, int eventId, int quantity);
    }
}
