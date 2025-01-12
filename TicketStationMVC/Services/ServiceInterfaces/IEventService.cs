using TicketStationMVC.Data.Entities;
using TicketStationMVC.ViewModels.Events;

namespace TicketStationMVC.Services.ServiceInterfaces
{
    public interface IEventService
    {
        public Task<Event> GetEventByIdAsync(int id);
        public Task<ICollection<Event>> GetAllEventsAsync();
        public Task<Event> CreateAsync(EventCreateVM @event);
        public Task<Event> UpdateAsync(EventEditVM @event, int userId);
        public Task<Event> DeleteAsync(int id);
        public Task<ICollection<Category>> GetCategoriesForEventAsync(int eventId);
    }
}
