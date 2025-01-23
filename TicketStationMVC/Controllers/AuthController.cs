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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace TicketStationMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationVM registrationVM)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }

                var existingUser = (await _userService
                    .GetAllUsersAsync())
                    .FirstOrDefault(u => u.Username == registrationVM.Username || u.Email == registrationVM.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", existingUser.Username == registrationVM.Username
                        ? "Username is already taken!"
                        : "User is already registered with this email!");
                    return View(registrationVM);
                }

                //user is not registered, register them
                //all newly registered users are assigned ordinary user role
                //viewmodel bc CreateUser in the service takes createviewmodel as parameter

                var userVm = new UserCreateVM()
                {
                    Name = registrationVM.Name,
                    Username = registrationVM.Username,
                    Email = registrationVM.Email,
                    Password = registrationVM.Password,
                    RoleId = 2
                };

                await _userService.CreateUser(userVm);
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Register[Post] action: {ex.Message}");
                return StatusCode(500);
            }
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
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (string.IsNullOrEmpty(loginVM.Username) || string.IsNullOrEmpty(loginVM.Password))
                {
                    _logger.LogWarning("Login attempt failed due to missing credentials. IP: {IP}, User-Agent: {UserAgent}", ipAddress, userAgent);
                    ModelState.AddModelError("", "Username/email and password should not empty");
                    return View();
                }

                //note: username field in the loginvm is for username OR email!
                //check is user exists

                var user = (await _userService.GetAllUsersAsync()).FirstOrDefault(u => u.Username.Equals(loginVM.Username) || u.Email.Equals(loginVM.Username));

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password))
                {
                    _logger.LogWarning("Failed login attempt for {Username}. IP: {IP}, User-Agent: {UserAgent}", loginVM.Username, ipAddress, userAgent);
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View();
                }
                //login successful
                var role = await _userService.GetRoleOfUserByIdAsync(user.Id);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FullName", user.Name),
                    new Claim(ClaimTypes.Role, role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                    IsPersistent = true, //if the browser is closed the cookie is not deleted it is stored locally
                    IssuedUtc = DateTime.UtcNow
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                _logger.LogInformation($"User {user.Email} logged in successfully at {DateTime.Now}. IP: {ipAddress}, User-Agent: {userAgent}");

                return RedirectToAction("Index", "Event");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Register[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await LogoutConfirmed();
            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}
