using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Repositories;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Cart;

namespace TicketStationMVC.Services
{
    public class CartService : ICartService
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IRepository<Cart> _cartRepository;
        private readonly ApplicationDbContext _context;
        public CartService(ApplicationDbContext context, IUserService userService, IEventService eventService, IRepository<Cart> repo)
        {
            _context = context;
            _userService = userService;
            _eventService = eventService;
            _cartRepository = repo;
        }

        public async Task<ICollection<Cart>> GetAllCartsAndCartItemsAsync()
        {
            return await _context.Carts.Include(c => c.CartItems).ToListAsync();
            //var allCarts = await _cartRepository.GetAllAsync();
            //var allCartsVM = new List<CartVM>();
            //foreach (var cart in allCarts)
            //{
            //    var newCartVM = new CartVM();
            //    newCartVM.Id = cart.Id;
            //    newCartVM.OwnerId = cart.OwnerId;
            //    var cartItemsVM = new List<CartItemVM>();

            //    foreach (var cartItem in cart.CartItems)
            //    {
            //        cartItemsVM.Add(new CartItemVM()
            //        {
            //            Id = cartItem.Id
            //        });
            //    }
            //}
        }
        public async Task<Cart?> EnsureUserHasCartAsync(int userId)
        {
            try
            {
                bool doesCurrentUserHaveCart = false;

                if (_cartRepository.GetAllAsync().Result.Count() > 0)
                {
                    doesCurrentUserHaveCart = _context.Carts.Where(c => c.OwnerId.Equals(userId)).FirstOrDefault() != null;
                }

                if (!doesCurrentUserHaveCart) //doesnt have cart, create one
                {
                    Cart cart = new Cart();
                    cart.OwnerId = userId;
                    cart.Owner = await _userService.GetUserByIdAsync(userId);
                    cart.CartItems = new List<CartItem>();
                    cart.CreatedAt = DateTime.Now;
                    cart.ModifiedAt = DateTime.Now;
                    await _cartRepository.CreateAsync(cart);
                    return cart;
                }

                return await _context.Carts.FirstOrDefaultAsync(x => x.OwnerId.Equals(userId));
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts?
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(c => c.OwnerId == userId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.Id.Equals(cartItemId));
        }

        public async Task<CartItem> IncreaseQuantityOfCartItemAsync(int cartItemId)
        {
            var item = await this.GetCartItemByIdAsync(cartItemId);
            var @event = await _eventService.GetEventByIdAsync(item.EventId);

            if (@event.Quantity - 1 >= 0)
            {
                item.Quantity += 1;
                @event.Quantity -= 1;
                _context.Events?.Update(@event);
                _context.CartItems?.Update(item);
                await _context.SaveChangesAsync();
            }

            return item;
        }

        public async Task<CartItem> DecreaseQuantityOfCartItemAsync(int cartItemId)
        {
            var item = await this.GetCartItemByIdAsync(cartItemId);
            var @event = await _eventService.GetEventByIdAsync(item.EventId);

            if (item.Quantity - 1 == 0)
            {
                _context.CartItems?.Remove(item);
                return null;
            }
            else
            {
                item.Quantity -= 1;
                @event.Quantity += 1;
                _context.Events?.Update(@event);
            }

            _context.CartItems?.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CartItem> RemoveCartItemFromCartAsync(int cartItemId)
        {
            var item = await this.GetCartItemByIdAsync(cartItemId);
            var @event = await _eventService.GetEventByIdAsync(item.EventId);

            @event.Quantity += item.Quantity;

            _context.Events?.Update(@event);
            _context.CartItems?.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<CartItem> AddItemToCartAsync(int cartId, int eventId, int quantity)
        {
            Event @event = await _eventService.GetEventByIdAsync(eventId);

            if (@event.Quantity > 0 && @event.Quantity - quantity >= 0)
            {

                var item = new CartItem()
                {
                    CartId = cartId,
                    EventId = eventId,
                    Quantity = quantity,
                    AddedToCart = DateTime.Now
                };

                _context.CartItems?.Add(item);
                @event.Quantity -= quantity;
                _context.Events?.Update(@event);
                await _context.SaveChangesAsync();
                return item;
            }

            throw new Exception("You cannot add more than the available quantity!");
        }

        public bool DoesUserHaveCart(int userId)
        {
            return _context.Carts.Any(x => x.OwnerId.Equals(userId));
        }
    }
}
