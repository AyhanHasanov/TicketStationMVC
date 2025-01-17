using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Repositories;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Cart;
using TicketStationMVC.ViewModels.User;

namespace TicketStationMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public UserService(IRepository<User> repo, ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _userRepository = repo;
            _context = context;
            _httpContextAccessor = contextAccessor;
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
        public async Task<User> DeleteUserWithCartsAndRestoreTicketsAsync(int id)
        {
            // Get the user with carts and cart items
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            var usersCart = await _context.Carts.Include(c=>c.CartItems).FirstOrDefaultAsync(x => x.OwnerId.Equals(id));

            if (usersCart != null && usersCart.CartItems != null)
                foreach (var cartItem in usersCart.CartItems)
                {
                    // Restore tickets
                    var @event = await _context.Events.FirstOrDefaultAsync(x => x.Id.Equals(cartItem.EventId));

                    @event.Quantity += cartItem.Quantity;

                    _context.Events?.Update(@event);
                    _context.CartItems?.Remove(cartItem);
                }
            await _context.SaveChangesAsync();

            // Delete the user and related entities
            return await _userRepository.DeleteAsync(user.Id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<Role> GetRoleOfUserByIdAsync(int id)
        {
            var user = await this.GetUserByIdAsync(id);
            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            return role;
        }

        public async Task<User> GetCurrentLoggedUserAsync()
        {
            var emailClaim = (_httpContextAccessor.HttpContext?.User)?.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));

            if (emailClaim == null)
                throw new Exception("no claims for this user");

            var user = await this.GetUserByEmailAsync(emailClaim.Value);
            return user;
        }
    }
}
