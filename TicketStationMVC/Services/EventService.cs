using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Repositories;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Events;

namespace TicketStationMVC.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Event> _eventsRepository;
        public EventService(IRepository<Event> repo, ApplicationDbContext context)
        {
            _eventsRepository = repo;
            _context = context;
        }
        public async Task<Event> CreateAsync(EventCreateVM createEventVM)
        {
            ICollection<EventCategories> eventCategories = new List<EventCategories>();

            foreach (var category in createEventVM.CategoryIds)
            {
                eventCategories.Add(new EventCategories() { EventId = createEventVM.Id, CategoryId = category });
            }

            Event @event = new Event()
            {
                Name = createEventVM.Name,
                Description = createEventVM.Description,
                Price = createEventVM.Price,
                Quantity = createEventVM.Quantity,
                DateOfEvent = createEventVM.DateOfEvent,
                Status = createEventVM.Status,
                EventCategories = eventCategories,
                ImageURL = createEventVM.ImageURL,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedById = createEventVM.CreatedById,
                ModifiedById = createEventVM.ModifiedById
            };

            var res = await _eventsRepository.CreateAsync(@event);
            return res;
        }

        public async Task<Event> DeleteAsync(int id)
        {
            return await _eventsRepository.DeleteAsync(id);
        }

        public async Task<ICollection<Event>> GetAllEventsAsync()
        {
            return await _eventsRepository.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventsRepository.GetByIdAsync(id);
        }
        
        public async Task<Event> UpdateAsync(EventEditVM eventEditVM, int userId)
        {

            var existingEvent = await this.GetEventByIdAsync(eventEditVM.Id);
            existingEvent.Name = eventEditVM.Name;
            existingEvent.Description = eventEditVM.Description;
            existingEvent.Quantity = eventEditVM.Quantity;
            existingEvent.Price = eventEditVM.Price;
            existingEvent.ModifiedAt = DateTime.Now;
            existingEvent.ModifiedById = userId;
            existingEvent.DateOfEvent = eventEditVM.DateOfEvent;
            existingEvent.Status = eventEditVM.Status;

            if (eventEditVM.ImageURL != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingEvent.ImageURL.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                existingEvent.ImageURL = eventEditVM.ImageURL;
            }
            
            existingEvent.EventCategories = new List<EventCategories>();

            var existingEventCategories = _context.EventCategories?
                .Where(ec => ec.EventId == existingEvent.Id);

            if (_context.EventCategories == null || existingEventCategories == null)
                throw new Exception("Empty");

            _context.EventCategories.RemoveRange(existingEventCategories);

            foreach (var item in eventEditVM.CategoryIds)
            {
                existingEvent.EventCategories
                    .Add(new EventCategories()
                    {
                        EventId = existingEvent.Id,
                        CategoryId = item
                    });
            }

            return await _eventsRepository.UpdateAsync(existingEvent);
        }

        public async Task<ICollection<Category>> GetCategoriesForEventAsync(int eventId)
        {
            var categories = await _context.EventCategories
               .Where(ec => ec.EventId == eventId)
               .Select(ec => ec.Category)
               .ToListAsync();

            return categories;
        }
    }
}
