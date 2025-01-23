using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketStationMVC.Data;
using TicketStationMVC.Data.Entities;
using TicketStationMVC.Services;
using TicketStationMVC.Services.ServiceInterfaces;
using TicketStationMVC.ViewModels.Category;

namespace TicketStationMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ApplicationDbContext context, ICategoryService service, ILogger<CategoryController> logger)
        {
            _context = context;
            _categoryService = service;
            _logger = logger;
        }

        // GET: CategoryController
        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Index()
        {
            var items = _categoryService.GetAllCategoriesAsync();
            return View(await items);
        }

        // GET: CategoryController/Details/5
        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.Categories == null)
                {
                    return NotFound();
                }

                var category = await _categoryService.GetCategoryByIdAsync(id.Value);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Details[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET: CategoryController/Create
        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM createVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Category wasn't created!";
                    return View(createVM);
                }

                if ((await _categoryService.GetAllCategoriesAsync()).Any(c => c.Name.ToLower().Equals(createVM.Name.ToLower())))
                {
                    ModelState.AddModelError("", "This ctageory already exists!");
                    return View(createVM);
                }

                await _categoryService.CreateAsync(createVM);
                TempData["SuccessMessage"] = "Category was created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Details[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
                return NotFound();

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category == null)
                return NotFound();

            CategoryVM vm = new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(vm);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryVM categoryVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Category wasn't edited!";
                    return View(categoryVM);
                }

                if (id != categoryVM.Id)
                {
                    return NotFound();
                }

                await _categoryService.UpdateAsync(categoryVM);

                TempData["SuccessMessage"] = "Category was edited successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(categoryVM.Id))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Edit[Post] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET: CategoryController/Delete/5
        [HttpGet]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || _context.Categories == null)
                    return NotFound();

                var category = await _categoryService.GetCategoryByIdAsync(id.Value);

                if (category == null)
                    return NotFound();

                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Delete[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (_context.Categories == null || id == null)
                {
                    return NotFound();
                }

                await _categoryService.DeleteAsync(id.Value);

                TempData["SuccessMessage"] = "Category was deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Details[Get] action: {ex.Message}");
                return StatusCode(500);
            }
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(c => c.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
