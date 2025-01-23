using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public HomeController(ILogger<HomeController> logger, IEventService eventService, IUserService userService)
        {
            _logger = logger;
            _eventService = eventService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
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

            ViewData["CountOfEvents"] = items.Count();
            ViewData["CountOfUser"] = (await _userService.GetAllUsersAsync()).Count();
            return View(result);
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
