using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandProject.Services.Interfaces;


namespace SecondHandProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;


        public AdminController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersForAdminAsync();
            return View(users);
        }



        // Ürün Yönetimi
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }


        // Kullanıcı Yönetimi
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsersForAdminAsync();
            return View(users);
        }


        // Admin ürün silerse HER YERDEN silinir
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Products));
        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Users));
        }
    }
}