using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Cart;

namespace TicketStationMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ApplicationDbContext context, IUserService userService, IEventService eventService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userService = userService;
            _eventService = eventService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByEmailAsync((_httpContextAccessor.HttpContext?.User)?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

            await EnsureUserHasCart.Ensure(_context, _userService, user.Id);

            var cart = _context.Carts.Include(x => x.CartItems).Where(c => c.OwnerId.Equals(user.Id)).FirstOrDefault();

            //var cartVM = new CartVM()
            //{
            //    Id = cart.Id,
            //    Owner = cart.Owner,
            //    OwnerId = cart.OwnerId,
            //    Items = new List<CartItemVM>()
            //};
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
            var item = await _context.CartItems.FindAsync(cartItemId);
            var @event = await _eventService.GetEventByIdAsync(item.EventId);

            if (@event.Quantity - 1 >= 0)
            {
                item.Quantity += 1;

                DecreaseEventQuantityFromDbWithOne(@event);

                _context.CartItems.Update(item);
                await _context.SaveChangesAsync();

                //CartItemVM cartItemViewModel = new CartItemVM()
                //{
                //    Id = item.Id,
                //    ImageUrl = @event.ImageURL,
                //    Name = @event.Name,
                //    Quantity = item.Quantity
                //};
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            var @event = await _eventService.GetEventByIdAsync(cartItem.EventId);

            cartItem.Quantity -= 1;

            if (cartItem.Quantity == 0)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            
            _context.CartItems.Update(cartItem);
            IncreaseEventQuantityFromDbWithOne(@event); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveEvent(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            var @event = await _eventService.GetEventByIdAsync(cartItem.EventId);

            @event.Quantity += cartItem.Quantity;

            _context.Events.Update(@event);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private void DecreaseEventQuantityFromDbWithOne(Event @event)
        {
            @event.Quantity -= 1;
            _context.Events.Update(@event);
            //await _context.SaveChangesAsync();
        }
        private void IncreaseEventQuantityFromDbWithOne(Event @event)
        {
            @event.Quantity += 1;
            _context.Events.Update(@event);
            //await _context.SaveChangesAsync();
        }

    }

}
