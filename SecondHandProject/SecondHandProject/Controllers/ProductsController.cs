using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandProject.Services.Interfaces;
using SecondHandProject.ViewModels.ProductViewModels;

namespace SecondHandProject.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public ProductsController(IProductService productService,
                                  ICategoryService categoryService,
                                  IUserService userService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            var userId = _userService.GetCurrentUserId(User);
            await _productService.CreateAsync(model, userId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _productService.GetEditModelAsync(id);
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            await _productService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productService.GetDetailsAsync(id);
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyProducts()
        {
            var userId = _userService.GetCurrentUserId(User);

            var products = await _productService.GetProductsByUserAsync(userId);

            return View(products);
        }

    }
}
