using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketStationMVC.Data;
using TicketStationMVC.Models;
using TicketStationMVC.Services;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Account;
using TicketStationMVC.ViewModels.Category;

namespace TicketStationMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger, IUserService userService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetCurrentLoggedUserAsync();

            if (_context.Users == null || user == null)
                return NotFound();

            var roleName = (await _userService.GetRoleOfUserByIdAsync(user.Id)).Name;

            var accountVm = new AccountDetailsVM()
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                RegisteredOn = user.RegisteredOn,
                RoleName = roleName
            };

            return View(accountVm);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userService.GetCurrentLoggedUserAsync();

            if (user == null)
                return NotFound();

            AccountChangePassVM vm = new AccountChangePassVM()
            {
                Id = user.Id
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(AccountChangePassVM changePassVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByIdAsync(changePassVM.Id);
                if (user == null)
                    return NotFound();

                if (BCrypt.Net.BCrypt.Verify(changePassVM.OldPass, user.Password))
                {
                    //old pass is correct change to new pass
                    if (changePassVM.NewPass.Equals(changePassVM.NewPassVerfication))
                    {
                        //new password is correctly typed twice
                        user.Password = BCrypt.Net.BCrypt.HashPassword(changePassVM.NewPass);
                        await _userService.UpdateAsync(user);

                        TempData["SuccessMessage"] = "Password changed successfully.";

                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ViewData["Error message"] = "The passwords do not match!";
                        ModelState.AddModelError("", "The new passwords do not match!");
                    }
                }
                else
                {
                    ViewData["Error message"] = "Old password is invalid!";
                    ModelState.AddModelError("", "Old password is invalid");
                }
            }
            return View();
        }

    }
}
