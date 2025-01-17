using TicketStationMVC.Data.Entities;
using TicketStationMVC.ViewModels.User;

namespace TicketStationMVC.Services.ServiceInterfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserDetailsByIdAsync(int id);
        public Task<ICollection<User>> GetAllUsersAsync();
        public Task<User> GetUserByEmailAsync(string email);
        public Task<User> CreateUser(UserCreateVM user);
        public Task<User> UpdateAsync(User user);
        public Task<User> DeleteAsync(int id);
        public Task<User> DeleteUserWithCartsAndRestoreTicketsAsync(int id);
        public Task<Role> GetRoleOfUserByIdAsync(int id);
        public Task<User> GetCurrentLoggedUserAsync();
    }
}
