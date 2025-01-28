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
using System.Drawing.Printing;

namespace TicketStationMVC.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventService eventService, ApplicationDbContext context, IUserService userService, ICartService cartService, ICategoryService categoryService, ILogger<EventController> logger)
        {
            _eventService = eventService;
            _context = context;
            _userService = userService;
            _cartService = cartService;
            _categoryService = categoryService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> AddEventToCart(int id, int quantity = 1)
        {
            try
            {
                var user = await _userService.GetCurrentLoggedUserAsync();

                if (user == null)
                {
                    return NotFound();
                }
                await _cartService.EnsureUserHasCartAsync(user.Id);

                int usersCartId = (await _cartService.GetCartByUserIdAsync(user.Id)).Id;

                var @event = await _eventService.GetEventByIdAsync(id);

                if (@event.Quantity <= 0)
                {
                    TempData["ErrorMessage"] = "The item was not added to your cart. There isn't enough quantity!";
                    return RedirectToAction(nameof(Index));
                }

                if (@event.Quantity < quantity)
                {
                    TempData["ErrorMessage"] = "You cannot add more than the available quantity!";
                    return RedirectToAction(nameof(Index));
                }

                var res = await _cartService.AddItemToCartAsync(usersCartId, id, quantity);

                if (res == null)
                    return BadRequest();

                TempData["SuccessMessage"] = "Item added successfully to your tickets";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(string eventFilter = "", int categoryId = -1, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                ViewData["CategoriesIdVD"] = new SelectList(_context.Set<Category>(), "Id", "Name");

                var result = await GetFilteredEvents(eventFilter, categoryId, startDate, endDate);

                if (startDate.HasValue)
                    ViewData["StartDateVD"] = startDate.Value.ToString("yyyy-MM-dd");

                if (endDate.HasValue)
                    ViewData["EndDateVD"] = endDate.Value.ToString("yyyy-MM-dd");

                try
                {
                    if (categoryId > 0)
                        ViewData["SelectedCategoryNameVD"] = (await _categoryService.GetCategoryByIdAsync(categoryId)).Name;
                }
                catch
                {
                    TempData["ErrorMessage"] = "Category doesn't exist";
                }

                ViewData["DelectedCategoryIdVD"] = categoryId;
                ViewData["FilterSearchVD"] = eventFilter; 

                return View(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> IndexForAdmins(string eventFilter = "", int categoryId = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                ViewData["CategoriesIdVD"] = new SelectList(_context.Set<Category>(), "Id", "Name");
                var res = await GetFilteredEvents(eventFilter, categoryId, startDate, endDate);

                if (categoryId > 0)
                    ViewData["SelectedCategoryIdVD"] = (await _categoryService.GetCategoryByIdAsync(categoryId)).Name;

                if (startDate.HasValue)
                    ViewData["StartDateVD"] = startDate.Value.ToString("yyyy-MM-dd");

                if (endDate.HasValue)
                    ViewData["EndDateVD"] = endDate.Value.ToString("yyyy-MM-dd");

                ViewData["FilterSearchVD"] = eventFilter;
                return View(res);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.Events == null)
                    return NotFound();

                var @event = await _eventService.GetEventByIdAsync(id.Value);

                if (@event == null)
                    return NotFound();

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
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in EventController Details[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoriesIdVD"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EventCreateVM createEventVM, IFormFile? imageFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Event wasn't created!";
                    return View(createEventVM);
                }

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
                    return NotFound();

                var user = await _userService.GetCurrentLoggedUserAsync();

                if (user == null)
                    return NotFound();

                createEventVM.CreatedById = user.Id;

                await _eventService.CreateAsync(createEventVM);
                TempData["SuccessMessage"] = "Event was successfully created.";
                return RedirectToAction(nameof(IndexForAdmins));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in EventController Create[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var @event = await _eventService.GetEventByIdAsync(id.Value);

                if (@event == null)
                    return NotFound();

                ViewData["CategoriesIdVD"] = new SelectList(_context.Set<Category>(), "Id", "Name");

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
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in EventController Edit[Get] action: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [FromForm] EventEditVM eventEditVM, IFormFile? imageFile)
        {
            try
            {
                ViewData["CategoriesIdVD"] = new SelectList(_context.Set<Category>(), "Id", "Name");

                if (id == null || _context.Events == null)
                    return NotFound();

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Event wasn't created!";
                    return View(eventEditVM);
                }

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
                return RedirectToAction(nameof(IndexForAdmins));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(eventEditVM.Id))
                {
                    return NotFound();
                }
                return StatusCode(500);
            }
            catch (Exception)
            {
                _logger.LogError($"Event edit update failed");
                return StatusCode(500);
            }
        }

        // GET: Events/Delete/5
        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || _context.Events == null)
                {
                    return NotFound();
                }

                var @event = await _eventService.GetEventByIdAsync(id.Value);

                if (@event == null)
                    return NotFound();

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
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Delete[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (id < 0 || _context.Events == null)
                {
                    return NotFound();
                }

                var eventToDelete = await _eventService.GetEventByIdAsync(id);

                if (eventToDelete == null)
                {
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
                return RedirectToAction(nameof(IndexForAdmins));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Details[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }

        private async Task<List<EventViewVM>> GetFilteredEvents(string eventFilter, int categoryId, DateTime? startDate, DateTime? endDate)
        {
            //// Base query
            //var query = _context.Events.AsQueryable();

            //// Apply filters
            //if (!string.IsNullOrEmpty(eventFilter))
            //    query = query.Where(e => e.Name.Contains(eventFilter));

            //if (categoryId > 0)
            //    query = query.Where(e => e.EventCategories.Any(c => c.CategoryId == categoryId));

            //if (startDate.HasValue)
            //    query = query.Where(e => e.DateOfEvent >= startDate.Value);

            //if (endDate.HasValue)
            //    query = query.Where(e => e.DateOfEvent <= endDate.Value);

            //// Total count before pagination
            //int totalCount = await query.CountAsync();

            //// Apply pagination
            //var items = await query
            //    .OrderBy(e => e.DateOfEvent) // Order by date or any other property
            //    .Skip((pageNumber - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            //var res = new List<EventViewVM>();
            //foreach (var item in items)
            //{
            //    var current = new EventViewVM()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Price = item.Price,
            //        DateOfEvent = item.DateOfEvent,
            //        ImageURL = item.ImageURL,
            //        Status = item.Status
            //    };
            //    _logger.LogInformation($"Price: {item.Price}, Type: {item.Price.GetType()}");

            //    var categories = (await _eventService.GetCategoriesForEventAsync(item.Id)).Select(es => es.Name).ToList();

            //    current.Categories = categories;

            //    res.Add(current);
            //}

            //return (res, totalCount);

            //var items = await _eventService.GetAllEventsAsync();

            //if (!string.IsNullOrEmpty(eventFilter))
            //{
            //    items = items.Where(e => e.Name.Contains(eventFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            //}

            //if (categoryId > 0)
            //{
            //    ICollection<Event> newItems = new List<Event>();
            //    foreach (var item in items)
            //    {
            //        var currEvent = await _eventService.GetCategoriesForEventAsync(item.Id);

            //        if (currEvent.Any(e => e.Id.Equals(categoryId)))
            //        {
            //            newItems.Add(item);
            //        }
            //    }
            //    items = newItems;
            //}

            //if (startDate.HasValue)
            //{
            //    items = items.Where(e => e.DateOfEvent >= startDate.Value).ToList();
            //    ViewData["startDate"] = startDate.Value.ToString("dd.MM.yyyy");
            //}

            //if (endDate.HasValue)
            //{
            //    items = items.Where(e => e.DateOfEvent <= endDate.Value).ToList();
            //    ViewData["endDate"] = endDate.Value.ToString("dd.MM.yyyy");
            //}

            //int totalCount = items.Count;

            //items = items
            //    .OrderBy(e=>e.DateOfEvent)
            //    .Skip((pageNumber-1) * pageSize)
            //    .Take(pageSize)
            //    .ToList();

            //var result = new List<EventViewVM>();

            //foreach (var item in items)
            //{
            //    var current = new EventViewVM()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Price = item.Price,
            //        DateOfEvent = item.DateOfEvent,
            //        ImageURL = item.ImageURL,
            //        Status = item.Status
            //    };

            //    var categories = (await _eventService.GetCategoriesForEventAsync(item.Id)).Select(es => es.Name).ToList();

            //    current.Categories = categories;

            //    result.Add(current);
            //}

            //return (result, totalCount);

            var items = await _eventService.GetAllEventsAsync();

            if (!string.IsNullOrEmpty(eventFilter))
            {
                items = items.Where(e => e.Name.Contains(eventFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (categoryId > 0)
            {
                ICollection<Event> newItems = new List<Event>();
                foreach (var item in items)
                {
                    var currEvent = await _eventService.GetCategoriesForEventAsync(item.Id);

                    if (currEvent.Any(e => e.Id.Equals(categoryId)))
                    {
                        newItems.Add(item);
                    }
                }
                items = newItems;
            }

            if (startDate.HasValue)
            {
                items = items.Where(e => e.DateOfEvent >= startDate.Value).ToList();
                ViewData["StartDateVD"] = startDate.Value.ToString("dd.MM.yyyy");
            }

            if (endDate.HasValue)
            {
                items = items.Where(e => e.DateOfEvent <= endDate.Value).ToList();
                ViewData["EndDateVD"] = endDate.Value.ToString("dd.MM.yyyy");
            }

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

            return result;
        }
    }
}
