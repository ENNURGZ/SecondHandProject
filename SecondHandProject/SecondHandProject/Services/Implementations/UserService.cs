using Microsoft.AspNetCore.Identity;
using SecondHandProject.Models.Entities;
using SecondHandProject.Services.Interfaces;
using SecondHandProject.ViewModels.AccountViewModels;

namespace SecondHandProject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetCurrentUserId(System.Security.Claims.ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user)!;
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            return Task.FromResult<IEnumerable<ApplicationUser>>(users);
        }
        public async Task<IEnumerable<UserAdminViewModel>> GetAllUsersForAdminAsync()
        {
            var users = _userManager.Users.ToList();

            var result = new List<UserAdminViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserAdminViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName ?? "-",
                    Email = user.Email ?? "-",
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }

            return result;
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
                await _userManager.DeleteAsync(user);
        }

    }
}
