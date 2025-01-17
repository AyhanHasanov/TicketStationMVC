using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Events;
using TicketStationMVC.ViewModels.User;
using TicketStationMVC.Services;
using TicketStationMVC.ViewModels.Category;

namespace TicketStationMVC.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public EventController(IEventService eventService, ApplicationDbContext context, IUserService userService, ICartService cartService)
        {
            _eventService = eventService;
            _context = context;
            _userService = userService;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> AddEventToCart(int id, int quantity = 1)
        {
            var user = await _userService.GetCurrentLoggedUserAsync();

            if (user == null)
                return NotFound();

            await _cartService.EnsureUserHasCartAsync(user.Id);

            int usersCartId = (await _cartService.GetCartByUserIdAsync(user.Id)).Id;
            try
            {
                var res = await _cartService.AddItemToCartAsync(usersCartId, id, quantity);

                if (res == null)
                    return BadRequest();
                TempData["SuccessMessage"] = "Item added successfully to your tickets";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var @event = await _eventService.GetEventByIdAsync(id.Value);

            EventDetailedVM detailedVM = new EventDetailedVM()
            {
                Id = id.Value,
                Name = @event.Name,
                Description = @event.Description,
                Price = @event.Price,
                Quantity = @event.Quantity,
                Status = @event.Status,
                DateOfEvent = @event.DateOfEvent,
                ImageURL = @event.ImageURL,
                CreatedByUsername = (await _userService.GetUserByIdAsync(@event.CreatedById)).Username,
                Categories = (await _eventService.GetCategoriesForEventAsync(@event.Id)).Select(es => es.Name).ToList()
            };

            return View(detailedVM);
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoriesID"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EventCreateVM createEventVM, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    //concatinates the date and time of the upload in order to avoid duplicated image names
                    var fileNameWithExtension = Path.GetFileName(imageFile.FileName);
                    var fileName = fileNameWithExtension.Split('.')[0].Trim() + DateTime.Now.ToString("yyyMMddHHmmssff");
                    var extension = fileNameWithExtension.Split('.')[1].Trim();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", String.Concat(fileName, '.', extension));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    createEventVM.ImageURL = "/image/" + String.Concat(fileName, '.', extension);
                }

                if (createEventVM == null)
                    return BadRequest();

                var user = await _userService.GetCurrentLoggedUserAsync();

                if (user == null)
                    return BadRequest();

                createEventVM.CreatedById = user.Id;

                await _eventService.CreateAsync(createEventVM);
                TempData["SuccessMessage"] = "Event was successfully created.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Event creation failed";
            return View();
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var @event = await _eventService.GetEventByIdAsync(id.Value);

            if (@event == null)
                return NotFound();

            ViewData["CategoriesID"] = new SelectList(_context.Set<Category>(), "Id", "Name");

            EventEditVM editVM = new EventEditVM()
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Status = @event.Status,
                DateOfEvent = @event.DateOfEvent,
                Quantity = @event.Quantity,
                Price = @event.Price,
                ImageURL = @event.ImageURL
            };

            editVM.CategoryIds = (await _eventService.GetCategoriesForEventAsync(@event.Id)).Select(es => es.Id).ToList();

            return View(editVM);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [FromForm] EventEditVM eventEditVM, IFormFile? imageFile)
        {
            ViewData["CategoriesID"] = new SelectList(_context.Set<Category>(), "Id", "Name");

            if (id == null || _context.Events == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.GetCurrentLoggedUserAsync();

                    if (user == null)
                        return NotFound();

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        //concatinates the date and time of the upload in order to avoid duplicated image names
                        var fileNameWithExtension = Path.GetFileName(imageFile.FileName);
                        var fileName = fileNameWithExtension.Split('.')[0].Trim() + DateTime.Now.ToString("yyyMMddHHmmssff");
                        var extension = fileNameWithExtension.Split('.')[1].Trim();
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", String.Concat(fileName, '.', extension));

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        eventEditVM.ImageURL = "/image/" + String.Concat(fileName, '.', extension);
                    }
                    await _eventService.UpdateAsync(eventEditVM, user.Id);
                    TempData["SuccessMessage"] = "Event was updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventEditVM.Id))
                    {
                        TempData["ErrorMessage"] = "Event was not found. Hence wasn't updated!";
                        return NotFound();
                    }
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Event update failed!";
            return View(eventEditVM);
        }

        // GET: Events/Delete/5
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _eventService.GetEventByIdAsync(id.Value);

            EventDetailedVM detailedVM = new EventDetailedVM()
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Price = @event.Price,
                Quantity = @event.Quantity,
                Status = @event.Status,
                DateOfEvent = @event.DateOfEvent,
                ImageURL = @event.ImageURL,
                CreatedByUsername = (await _userService.GetUserByIdAsync(@event.CreatedById)).Username,
                Categories = (await _eventService.GetCategoriesForEventAsync(@event.Id)).Select(es => es.Name).ToList()
            };
            return View(detailedVM);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventToDelete = await _eventService.GetEventByIdAsync(id);
            if (eventToDelete == null)
            {
                TempData["ErrorMessage"] = "Event was not found. Hence wasn't deleted!";
                return NotFound();

            }

            // Get the image file path
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventToDelete.ImageURL.TrimStart('/'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            await _eventService.DeleteAsync(eventToDelete.Id);
            TempData["SuccessMessage"] = "Event was deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
