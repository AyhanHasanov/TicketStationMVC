using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketStationMVC.Data;
using TicketStationMVC.Models;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Events;

namespace TicketStationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IEventService eventService, IUserService userService, ApplicationDbContext context)
        {
            _logger = logger;
            _eventService = eventService;
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var items = await _eventService.GetAllEventsAsync();

                var result = new List<EventViewVM>();
                foreach (var item in items)
                {
                    var current = new EventViewVM()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        DateOfEvent = item.DateOfEvent,
                        ImageURL = item.ImageURL,
                        Status = item.Status
                    };

                    var categories = (await _eventService.GetCategoriesForEventAsync(item.Id)).Select(es => es.Name).ToList();

                    current.Categories = categories;

                    result.Add(current);
                }

                ViewData["CountOfEventsVD"] = items.Where(x => x.Status).Count();
                ViewData["CountOfUsersVD"] = (await _userService.GetAllUsersAsync()).Count();
                int totalTickets = 0;
                foreach (var ticket in _context.CartItems)
                {
                    totalTickets += ticket.Quantity;
                }
                ViewData["CountOfTicketsVD"] = totalTickets;
                return View(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
