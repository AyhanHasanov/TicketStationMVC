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
        private readonly ICategoryService _categoryService;

        public CategoryController(ApplicationDbContext context, ICategoryService service)
        {
            _context = context;
            _categoryService = service;
        }

        // GET: CategoryController
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Index()
        {
            var items = _categoryService.GetAllCategoriesAsync();
            return View(await items);
        }

        // GET: CategoryController/Details/5
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
                return NotFound();

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // GET: CategoryController/Create
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM createVM)
        {
            if (ModelState.IsValid)
            {
                if ((await _categoryService.GetAllCategoriesAsync()).Any(c => c.Name.ToLower().Equals(createVM.Name.ToLower())))
                {
                    ModelState.AddModelError("", "This ctageory already exists!");
                    return View(createVM);
                }

                await _categoryService.CreateAsync(createVM);
                return RedirectToAction(nameof(Index));
            }
            return View(createVM);
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        [Authorize]
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
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryVM categoryVM)
        {
            if (id != categoryVM.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(categoryVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryVM.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        // GET: CategoryController/Delete/5
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
                return NotFound();

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [Authorize(Roles = "adminuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Categories == null || id == null)
                return NotFound();

            await _categoryService.DeleteAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(c => c.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
