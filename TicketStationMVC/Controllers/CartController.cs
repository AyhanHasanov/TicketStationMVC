using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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
        public async Task<IActionResult> AllTickets(string usernameFilter = "", string emailFilter = "")
        {
            try
            {
                var result = new List<AllCartItemsVM>();

                foreach (var cartCartItems in (await _cartService.GetAllCartsAndCartItemsAsync()).Where(x=>x.OwnerId != null).ToList())
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
                    ViewData["UsernameVD"] = usernameFilter;
                }
                if (!string.IsNullOrEmpty(emailFilter))
                {
                    result = result.Where(r => r.OwnerEmail.Contains(emailFilter, StringComparison.OrdinalIgnoreCase)).ToList();
                    ViewData["EmailVD"] = emailFilter;
                }


                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in CartController, AllTickets [get] action: {ex.Message}");
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

                if (user == null)
                {
                    return NotFound();
                }

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


            ViewData["TotalVD"] = total;

                return View(cartVM);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in CartController Index[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [Authorize]
        public async Task<IActionResult> IncreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.IncreaseQuantityOfCartItemAsync(cartItemId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in CartConrtoller IncreaseQuantity[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [Authorize]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.DecreaseQuantityOfCartItemAsync(cartItemId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in CartConrtoller DecreaseQuantity[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [Authorize]
        public async Task<IActionResult> RemoveEvent(int cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItemFromCartAsync(cartItemId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in CartController RemoveEvent[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
