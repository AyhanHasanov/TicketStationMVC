using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.User;

namespace TicketStationMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;
        public UserController(IUserService userService, ApplicationDbContext context, ICartService cartService, ILogger<UserController> logger)
        {
            _userService = userService;
            _context = context;
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Index(string usernameFilter = "", string emailFilter = "", int roleIdFilter = -1)
        {
            try
            {
                ViewData["RolesIdVD"] = new SelectList(_context.Set<Role>(), "Id", "Name");

                var users = await _userService.GetAllUsersAsync();

                if (!string.IsNullOrEmpty(usernameFilter))
                {
                    users = users.Where(u => u.Username.Contains(usernameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

                    ViewData["UsernameVD"] = usernameFilter;
                }

                if (!string.IsNullOrEmpty(emailFilter))
                {
                    users = users.Where(u => u.Email.Contains(emailFilter, StringComparison.OrdinalIgnoreCase)).ToList();

                    ViewData["EmailVD"] = emailFilter;
                }

                if (roleIdFilter > 0)
                {
                    users = users.Where(u => u.RoleId.Equals(roleIdFilter)).ToList();
                    ViewData["RoleNameVD"] = (await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleIdFilter)).Name;
                }


                List<UserViewVM> userViews = new List<UserViewVM>();
                foreach (var user in users)
                {
                    var roleName = await GetRoleNameByUserId(user.Id);

                    userViews.Add(new UserViewVM()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Name = user.Name,
                        Email = user.Email,
                        RoleName = roleName
                    });
                }
                return View(userViews);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in UserController Index action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                    return NotFound();

                var roleName = await GetRoleNameByUserId(user.Id);

                UserDetailsVM detailsVM = new UserDetailsVM()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Name = user.Name,
                    RoleName = roleName,
                    RegisteredOn = user.RegisteredOn
                };

                return View(detailsVM);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in UserController Details[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Create()
        {
            ViewData["RoleIdVD"] = new SelectList(_context.Set<Role>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateVM userCreateVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ViewData["RoleIdVD"] = new SelectList(_context.Set<Role>(), "Id", "Name");

                if ((await _userService.GetAllUsersAsync()).Any(u => u.Email.Equals(userCreateVM.Email)))
                {
                    ModelState.AddModelError("", "A user with this email already exists!.");
                    return View(userCreateVM);
                }

                await _userService.CreateUser(userCreateVM);
                _logger.LogInformation($"New user created: {userCreateVM.Email}");
                ViewData["SuccessMessages"] = "A new user was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in UserController Create[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                ViewData["RoleIdVD"] = new SelectList(_context.Set<Role>(), "Id", "Name");

                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                    return NotFound();

                var userVm = new UserEditVM()
                {
                    Name = user.Name,
                    Username = user.Username,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    Password = user.Password
                };

                return View(userVm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in UserController Edit[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editVm)
        {
            try
            {
                ViewData["RoleIdVD"] = new SelectList(_context.Set<Role>(), "Id", "Name");
                if (!ModelState.IsValid)
                {
                    return View(editVm);
                }

                var existingUser = await _userService.GetUserByIdAsync(editVm.Id);

                if (existingUser == null || editVm == null)
                {
                    TempData["ErrorMessage"] = "User not found!";
                    return NotFound();
                }

                existingUser.Name = editVm.Name;
                existingUser.Username = editVm.Username;
                existingUser.Email = editVm.Email;
                existingUser.RoleId = editVm.RoleId;
                existingUser.ModifiedAt = DateTime.Now;

                if (!string.IsNullOrEmpty(editVm.Password))
                    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(editVm.Password);

                await _userService.UpdateAsync(existingUser);

                TempData["SuccessMessage"] = "User was updated successfully!";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in UserController Edit[Post] action: {ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest();

                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                    return NotFound();

                var roleName = await GetRoleNameByUserId(user.Id);

                var userVM = new UserDetailsVM()
                {
                    Id = id,
                    Name = user.Name,
                    Email = user.Email,
                    Username = user.Username,
                    RegisteredOn = user.RegisteredOn,
                    RoleName = roleName
                };

                return View(userVM);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in UserController Delete[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                if (_cartService.DoesUserHaveCart(user.Id))
                {
                    await _userService.DeleteUserWithCartsAndRestoreTicketsAsync(user.Id);
                    ViewData["SuccessMessage"] = "User was deleted successfully!";
                }
                else
                {   //if user doesnt have a cart
                    await _userService.DeleteAsync(id);
                    ViewData["SuccessMessage"] = "User was deleted successfully!";
                }

                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "User wasn't deleted successfully!";
                _logger.LogError($"Error occured in UserController Delete[Post] action: {ex.Message}");
                return StatusCode(500);
            }

        }
        private async Task<string> GetRoleNameByUserId(int userId)
        {
            try
            {
                return (await _userService.GetRoleOfUserByIdAsync(userId))?.Name ?? "not available";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving role for user {userId}: {ex.Message}");
                return "Error retrieving role";
            }
        }
    }
}
