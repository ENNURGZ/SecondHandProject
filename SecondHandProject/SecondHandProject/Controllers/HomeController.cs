using Microsoft.AspNetCore.Mvc;
using SecondHandProject.Services.Interfaces;

namespace SecondHandProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // ANASAYFA
        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryService.GetAllAsync();

            // Kategori filtresi varsa sadele?tir
            if (categoryId.HasValue)
            {
                var filteredProducts = await _productService.GetProductsByCategoryAsync(categoryId.Value);
                ViewBag.Categories = categories;
                ViewBag.SelectedCategory = categoryId;

                return View(filteredProducts);
            }

            var products = await _productService.GetAllAsync();
            ViewBag.Categories = categories;

            return View(products);
        }

        // Privacy Page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
