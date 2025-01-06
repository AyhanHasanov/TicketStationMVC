using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Data;
using TicketStationMVC.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Services.ServiceInterfaces;

namespace TicketStationMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ApplicationDbContext context, ILogger<AuthController> logger, IUserService userService)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationVM registrationVM)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Username.Equals(registrationVM.Username)))
                {
                    //username is taken
                    ModelState.AddModelError("", "Username is already taken!");
                    return View(registrationVM);
                }

                if (await _context.Users.AnyAsync(u => u.Email.Equals(registrationVM.Email)))
                {
                    //user is already registered
                    ModelState.AddModelError("", "User is already registered with this email!");
                    return View(registrationVM);
                }

                //user is not registered, register them
                string hashedPass = BCrypt.Net.BCrypt.HashPassword(registrationVM.Password);

                var user = new User()
                {
                    Username = registrationVM.Username,
                    Email = registrationVM.Email,
                    Password = hashedPass,
                    Name = registrationVM.Name,
                    RegisteredOn = DateTime.UtcNow,
                    RoleId = 2,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(loginVM.Username) || string.IsNullOrEmpty(loginVM.Password))
                {
                    ModelState.AddModelError("", "Username/email and password should not empty");
                    return View();
                }

                //note: username field in the loginvm is for username OR email!
                //check is user exists

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(loginVM.Username) || u.Email.Equals(loginVM.Username));

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }

                if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password))
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }
                //login successful
                var role = _context.Roles.Single(r => r.Id.Equals(user.RoleId)).Name;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FullName", user.Name),
                    new Claim(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(25),
                    IsPersistent = true, //if the browser is closed the cookie is not deleted it is stored locally
                    IssuedUtc = DateTime.UtcNow
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                _logger.LogInformation("User {Email} has logged in at {Time}", user.Email, DateTime.UtcNow);

                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}
