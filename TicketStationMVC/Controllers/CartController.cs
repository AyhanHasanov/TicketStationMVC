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
        private readonly ILogger<CartController> _logger;

        public CartController(IUserService userService, IEventService eventService, ICartService cartService, ILogger<CartController> logger)
        {
            _userService = userService;
            _eventService = eventService;
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> AllTickets(string usernameFilter = "")
        {
            try
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

                if (!string.IsNullOrEmpty(usernameFilter))
                {
                    result = result.Where(r => r.OwnerUsername.Contains(usernameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
                    ViewData["usernameVD"] = usernameFilter;
                }

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in AllTickets[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userService.GetCurrentLoggedUserAsync();
                await _cartService.EnsureUserHasCartAsync(user.Id);

                var cart = await _cartService.GetCartByUserIdAsync(user.Id);

                var cartVM = new CartVM
                {
                    Id = cart.Id,
                    Owner = cart.Owner,
                    OwnerId = cart.OwnerId,
                    Items = new List<CartItemVM>()
                };

                decimal total = 0m;

                foreach (var item in cart.CartItems)
                {
                    Event @event = await _eventService.GetEventByIdAsync(item.EventId);
                    CartItemVM cartItemVM = new CartItemVM
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
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Index[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [Authorize]
        public async Task<IActionResult> IncreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.IncreaseQuantityOfCartItemAsync(cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in IncreaseQuantity[post] action: {ex.Message}");
                return StatusCode(500);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.DecreaseQuantityOfCartItemAsync(cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in DecreaseQuantity[post] action: {ex.Message}");
                return StatusCode(500);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> RemoveEvent(int cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItemFromCartAsync(cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in RemoveEvent[post] action: {ex.Message}");
                return StatusCode(500);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
