using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Repositories;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.User;

namespace TicketStationMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ApplicationDbContext _context;
        public UserService(IRepository<User> repo, ApplicationDbContext context)
        {
            _userRepository = repo;
            _context = context;
        }
        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserDetailsByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<User> CreateUser(UserCreateVM userCreateVM)
        {
            User user = new User()
            {
                Name = userCreateVM.Name,
                Username = userCreateVM.Username,
                Email = userCreateVM.Email,
                RegisteredOn = DateTime.Now,
                RoleId = userCreateVM.RoleId,
                Password = BCrypt.Net.BCrypt.HashPassword(userCreateVM.Password),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            return await _userRepository.CreateAsync(user);
        }

        public async Task<User> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}
