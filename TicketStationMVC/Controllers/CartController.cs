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
                    Price = @event.Price
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
            return RedirectToAction(nameof(Index));
        }
    }
}
