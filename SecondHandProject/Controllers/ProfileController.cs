using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHandProject.Data;
using SecondHandProject.Models.Entities;
using SecondHandProject.ViewModels.Profile;
using System;

namespace SecondHandProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var products = await _context.Products
                .Where(p => p.UserId == user.Id)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new MyProductVm
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            var vm = new ProfileIndexVm
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                ProfileImagePath = user.ProfileImagePath,
                Products = products
            };

            return View(vm);
        }

        // GET: Profile/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            return View(new ProfileEditVm
            {
                FullName = user.FullName,
                Email = user.Email
            });
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileEditVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            TempData["Success"] = "Profil bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Profile/ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            TempData["Success"] = "Şifre başarıyla değiştirildi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
