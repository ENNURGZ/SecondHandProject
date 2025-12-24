using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandProject.Models.Entities;
using SecondHandProject.Services.Interfaces;
using SecondHandProject.ViewModels;

namespace SecondHandProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // USER - CATEGORY LIST (Home tarafı)
        [AllowAnonymous]
        public async Task<IActionResult> IndexUser()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // ADMIN - LIST
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // ADMIN - CREATE (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // ADMIN - CREATE (POST)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new Category
            {
                Name = model.Name
            };

            await _categoryService.CreateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        // ADMIN - EDIT (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var vm = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(vm);
        }

        // ADMIN - EDIT (POST)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new Category
            {
                Id = model.Id,
                Name = model.Name
            };

            await _categoryService.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }

        // ADMIN - DELETE (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var hasProducts = await _categoryService.HasProductsAsync(id);

            var vm = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                HasProducts = hasProducts
            };

            return View(vm);
        }

        // ADMIN - DELETE (POST)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hasProducts = await _categoryService.HasProductsAsync(id);

            if (hasProducts)
            {
                TempData["Error"] =
                    "Bu kategoriye ait ürünler bulunduğu için silinemez.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
