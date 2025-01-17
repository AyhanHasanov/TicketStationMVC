using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Cart;

namespace TicketStationMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly ICartService _cartService;

        public CartController(IUserService userService, IEventService eventService, ICartService cartService)
        {
            _userService = userService;
            _eventService = eventService;
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> AllTickets()
        {
            var result = new List<AllCartItemsVM>();
            foreach (var cartCartItems in await _cartService.GetAllCartsAndCartItemsAsync())
            {
                var user = await _userService.GetUserByIdAsync(cartCartItems.OwnerId.Value);
                //takes the cart part
                var cartVM = new AllCartItemsVM();
                cartVM.Id = cartCartItems.Id;
                cartVM.OwnerId = cartCartItems.OwnerId;
                cartVM.OwnerUsername = user.Username;
                cartVM.OwnerEmail = user.Email;
                cartVM.Items = new List<CartItemVM>();

                foreach (var cartItem in cartCartItems.CartItems)
                {
                    Event @event = await _eventService.GetEventByIdAsync(cartItem.EventId);
                    var cartItemVM = new CartItemVM()
                    {
                        Id = cartItem.Id,
                        ImageUrl = @event.ImageURL,
                        Name = @event.Name,
                        Quantity = cartItem.Quantity,
                        Price = @event.Price,
                        AddedToCart = cartItem.AddedToCart
                    };
                    cartVM.Items.Add(cartItemVM);
                }
                result.Add(cartVM);
            }
            return View(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetCurrentLoggedUserAsync();

            await _cartService.EnsureUserHasCartAsync(user.Id);

            var cart = await _cartService.GetCartByUserIdAsync(user.Id);

            var cartVM = new CartVM();
            cartVM.Id = cart.Id;
            cartVM.Owner = cart.Owner;
            cartVM.OwnerId = cart.OwnerId;
            cartVM.Items = new List<CartItemVM>();

            decimal total = 0m;

            foreach (var item in cart.CartItems)
            {
                Event @event = await _eventService.GetEventByIdAsync(item.EventId);
                CartItemVM cartItemVM = new CartItemVM()
                {
                    Id = item.Id,
                    ImageUrl = @event.ImageURL,
                    Name = @event.Name,
                    Quantity = item.Quantity,
                    Price = @event.Price,
                    AddedToCart = item.AddedToCart
                };

                cartVM.Items.Add(cartItemVM);
                total += (cartItemVM.Quantity * cartItemVM.Price);
            }
            ViewData["Total"] = total;

            return View(cartVM);
        }

        public async Task<IActionResult> IncreaseQuantity(int cartItemId)
        {
            await _cartService.IncreaseQuantityOfCartItemAsync(cartItemId);
            ViewData["SuccessMessage"] = "Increased quantity successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            await _cartService.DecreaseQuantityOfCartItemAsync(cartItemId);
            ViewData["SuccessMessage"] = "Decreased quantity successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveEvent(int cartItemId)
        {
            await _cartService.RemoveCartItemFromCartAsync(cartItemId);
            ViewData["SuccessMessage"] = "The event was successfully removed from your cart!";
            return RedirectToAction(nameof(Index));
        }
    }
}
