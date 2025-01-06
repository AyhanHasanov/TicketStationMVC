using TicketStationMVC.Data.Entities;
using TicketStationMVC.Repositories;
using TicketStationMVC.Services.ServiceInterfaces;

namespace TicketStationMVC.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventsRepository;

        public EventService(IRepository<Event> repo)
        {
            _eventsRepository = repo;
        }
        public async Task<Event> CreateAsync(Event @event)
        {
            return await _eventsRepository.CreateAsync(@event);
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
    }
}
