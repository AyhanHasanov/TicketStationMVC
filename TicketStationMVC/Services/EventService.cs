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
                CreatedById = createEventVM.CreatedById
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

        public async Task<Event> UpdateAsync(Event @event)
        {
            return await _eventsRepository.UpdateAsync(@event);
        }

        public async Task<ICollection<string>> GetCategoriesForEventAsync(int eventId)
        {
             var categories = await _context.EventCategories
                .Where(ec => ec.EventId == eventId)
                .Select(ec => ec.Category.Name) 
                .ToListAsync();

            return categories;
        }
    }
}
