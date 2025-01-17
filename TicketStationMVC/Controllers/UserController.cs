using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.User;

namespace TicketStationMVC.Controllers
{
    public class UserController : Controller
    {
        //Only the admins can create, view, edit or delete users

        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly ApplicationDbContext _context;
        public UserController(IUserService userService, ApplicationDbContext context, ICartService cartService)
        {
            _userService = userService;
            _context = context;
            _cartService = cartService;
        }
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            List<UserViewVM> userViews = new List<UserViewVM>();
            foreach (var user in users)
            {
                var roleName = (await _userService.GetRoleOfUserByIdAsync(user.Id)).Name;
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

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                    return NotFound();

                var roleName = (await _userService.GetRoleOfUserByIdAsync(user.Id)).Name;

                UserDetailsVM detailsVM = new UserDetailsVM()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Name = user.Name,
                    RoleName = roleName == null ? "not available" : roleName,
                    RegisteredOn = user.RegisteredOn
                };

                return View(detailsVM);
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateVM userCreateVM)
        {
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "Id", "Name");
            if (ModelState.IsValid)
            {
                if ((await _userService.GetAllUsersAsync()).Any(u => u.Email.Equals(userCreateVM.Email)))
                {
                    ModelState.AddModelError("", "There is already a user with this email.");
                    ViewData["ErrorMessage"] = "A user with this email already exists!";
                    return View(userCreateVM);
                }

                await _userService.CreateUser(userCreateVM);
                ViewData["SuccessMessages"] = "A new user was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "Id", "Name");
            var user = await _userService.GetUserByIdAsync(id);

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

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editVm)
        {
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "Id", "Name");

            if (ModelState.IsValid)
            {
                if (editVm == null)
                {
                    TempData["ErrorMessage"] = "User not found!";
                    return NotFound();
                }

                var existingUser = await _userService.GetUserByIdAsync(editVm.Id);

                if (existingUser == null)
                {
                    TempData["ErrorMessage"] = "User not found!";
                    return NotFound();
                }


                existingUser.Name = editVm.Name;
                existingUser.Username = editVm.Username;
                existingUser.Email = editVm.Email;
                existingUser.RoleId = editVm.RoleId;
                existingUser.ModifiedAt = DateTime.Now;
                existingUser.Password = editVm.Password == null ? existingUser.Password : BCrypt.Net.BCrypt.HashPassword(editVm.Password);

                await _userService.UpdateAsync(existingUser);

                TempData["SuccessMessage"] = "User was updated successfully!";
                return RedirectToAction(nameof(Index));

            }
            return View(editVm);
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var roleName = (await _userService.GetRoleOfUserByIdAsync(user.Id)).Name;

            if (roleName == null)
                roleName = "not available";

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

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserDetailsVM userVM)
        {
            var user = await _userService.GetUserByIdAsync(userVM.Id);

            if (user == null)
            {
                ViewData["ErrorMessage"] = "User wasn't deleted!";
                return NotFound();
            }

            if (_cartService.DoesUserHaveCart(user.Id))
            {
                await _userService.DeleteUserWithCartsAndRestoreTicketsAsync(user.Id);
                ViewData["SuccessMessage"] = "User was deleted successfully!";
            }
            else
            {   //if user doesnt have a cart
                await _userService.DeleteAsync(userVM.Id); 
                ViewData["SuccessMessage"] = "User was deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
