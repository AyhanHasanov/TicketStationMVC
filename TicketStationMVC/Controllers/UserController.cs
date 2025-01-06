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
        //access to this controller will have only the admins
        //Only the admins can create, view, edit or delete users

        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        public UserController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }
        // GET: UserController
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Index()
        {
            var items = await (from user in _context.Users
                               join role in _context.Roles
                               on user.RoleId equals role.Id
                               select new UserViewVM
                               {
                                   Id = user.Id,
                                   Username = user.Username,
                                   Name = user.Name,
                                   Email = user.Email,
                                   RoleName = role.Name
                               }).ToListAsync();

            return View(items);
        }

        // GET: UserController/Details/5
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

                var roleName = _context.Roles?.FirstOrDefault(r => r.Id == user.RoleId).Name;

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

        // GET: UserController/Create
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
            if (ModelState.IsValid)
            {
                if ((await _userService.GetAllUsersAsync()).Any(u => u.Email.Equals(userCreateVM.Email)))
                {
                    ModelState.AddModelError("", "There is already a user with this email.");
                    return View(userCreateVM);
                }

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

                await _userService.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: UserController/Edit/5
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["RoleId"] = new SelectList(_context.Set<Role>(), "Id", "Name");
            var user = await _userService.GetUserByIdAsync(id);

            //mapping
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

        // POST: UserController/Edit/5
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editVm)
        {
            if (ModelState.IsValid)
            {
                if (editVm == null)
                    return NotFound();

                var existingUser = await _userService.GetUserByIdAsync(editVm.Id);

                if (existingUser == null)
                    return NotFound();


                existingUser.Name = editVm.Name;
                existingUser.Username = editVm.Username;
                existingUser.Email = editVm.Email;
                existingUser.RoleId = editVm.RoleId;
                existingUser.ModifiedAt = DateTime.Now;
                existingUser.Password = editVm.Password == null ? existingUser.Password : BCrypt.Net.BCrypt.HashPassword(editVm.Password);

                await _userService.UpdateAsync(existingUser);
                return RedirectToAction(nameof(Index));

            }
            return View(editVm);
        }

        // GET: UserController/Delete/5
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var roleName = (await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId)).Name;

            if (roleName == null)
                return NotFound();

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

        // POST: UserController/Delete/5
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserDetailsVM userVM)
        {
            var user = await _userService.GetUserByIdAsync(userVM.Id);

            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(userVM.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
