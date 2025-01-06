using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Events;

namespace TicketStationMVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventsController(IEventService eventService, IHttpContextAccessor httpContextAccessor)
        {
            _eventService = eventService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Events
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return default;
            //    var applicationDbContext = _context.Events.Include(@ => @.CreatedBy);
            //    return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return default;
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var @event = await _context.Events
            //    .Include(@ => @.CreatedBy)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (@event == null)
            //{
            //    return NotFound();
            //}

            //return View(@event);
        }

        // GET: Events/Create
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Create()
        {

            return default;
            //ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Email");
            //return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateVM vm)
        {
            return View();

            //if (ModelState.IsValid)
            //{
            //    if(vm == null)
            //        return BadRequest(ModelState);

            //    var userEmail = (_httpContextAccessor.HttpContext?.User)?.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.Email)?.Value;

            //    var user = _

            //    Event @event = new Event()
            //    {
            //        Name = vm.Name,
            //        Description = vm.Description,
            //        DateOfEvent = vm.DateOfEvent,
            //        ImageURL = vm.ImageURL,
            //        Status = vm.Status,
            //        Price = vm.Price,
            //        Quantity = vm.Quantity,
            //        CreatedAt = DateTime.Now,
            //        CreatedById = _httpContextAccessor.HttpContext?.User
            //    };
            //}

            //if (ModelState.IsValid)
            //{

            //}
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
        public async Task<IActionResult> Delete(int? id)
        {
            return default;
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var @event = await _context.Events
            //    .Include(@ => @.CreatedBy)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (@event == null)
            //{
            //    return NotFound();
            //}

            //return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return default;
            //    var @event = await _context.Events.FindAsync(id);
            //    if (@event != null)
            //    {
            //        _context.Events.Remove(@event);
            //    }

            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return default;
        }
    }
}
