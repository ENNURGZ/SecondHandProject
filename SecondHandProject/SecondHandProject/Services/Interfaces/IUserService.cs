using SecondHandProject.Models.Entities;
using SecondHandProject.ViewModels.AccountViewModels;

namespace SecondHandProject.Services.Interfaces
{
    public interface IUserService
    {
        string GetCurrentUserId(System.Security.Claims.ClaimsPrincipal user);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IEnumerable<UserAdminViewModel>> GetAllUsersForAdminAsync();

        Task DeleteUserAsync(string id);
    }
}
