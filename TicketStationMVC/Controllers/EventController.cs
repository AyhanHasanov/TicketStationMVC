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

            var vmMaps = items.Select(x => new EventViewVM()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                DateOfEvent = x.DateOfEvent,
                ImageURL = x.ImageURL,
                Categories = _eventService.GetCategoriesForEventAsync(x.Id).Result
            });

            return View(vmMaps);
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
                Categories = _eventService.GetCategoriesForEventAsync(@event.Id).Result
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
            return default;
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var @event = await _context.Events.FindAsync(id);
            //if (@event == null)
            //{
            //    return NotFound();
            //}
            //ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Email", @event.CreatedById);
            //return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Quantity,DateOfEvent,CreatedById,Status,ImageURL,CreatedAt,ModifiedAt")] Event @event)
        {
            return default;
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
                Categories = _eventService.GetCategoriesForEventAsync(@event.Id).Result
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
            return default;
        }
    }
}
