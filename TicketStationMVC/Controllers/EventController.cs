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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public EventController(IEventService eventService, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IUserService userService)
        {
            _eventService = eventService;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userService = userService;
        }

        // GET: Events
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
                    ImageURL = item.ImageURL
                };

                var categories = (await _eventService.GetCategoriesForEventAsync(item.Id)).Select(es => es.Name).ToList();

                current.Categories = categories;

                result.Add(current);
            }

            //var vmMaps = items.Select(x => new EventViewVM()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Price = x.Price,
            //    DateOfEvent = x.DateOfEvent,
            //    ImageURL = x.ImageURL
            //});

            //foreach (var item in vmMaps)
            //{
            //    item.Categories = (await _eventService.GetCategoriesForEventAsync(item.Id)).Select(es => es.Name).ToList();
            //}

            return View(result);
        }

        [HttpGet]
        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var @event = await _eventService.GetEventByIdAsync(id.Value);

            //mapp

            EventDetailedVM detailedVM = new EventDetailedVM()
            {
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

        // GET: Events/Create
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoriesID"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                var user = await _userService.GetUserByEmailAsync((_httpContextAccessor.HttpContext?.User)?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

                if (user == null)
                    return BadRequest();

                createEventVM.CreatedById = user.Id;

                await _eventService.CreateAsync(createEventVM);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Events/Edit/5
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

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    var user = _userService.GetUserByEmailAsync((_httpContextAccessor.HttpContext.User)
                        ?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

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


                    await _eventService.UpdateAsync(eventEditVM, user.Result.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventEditVM.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(eventEditVM);

            //if (id != @event.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(@event);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!EventExists(@event.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Email", @event.CreatedById);
            //return View(@event);
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

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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

            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
