using TicketStationMVC.Data.Entities;

namespace TicketStationMVC.Services.ServiceInterfaces
{
    public interface IEventService
    {
        public Task<Event> GetEventByIdAsync(int id);
        public Task<ICollection<Event>> GetAllEventsAsync();
        public Task<Event> CreateAsync(Event @event);
        public Task<Event> UpdateAsync(Event @event);
        public Task<Event> DeleteAsync(int id);
        public Task<ICollection<string>> GetCategoriesForEventAsync(int eventId);
    }
}
