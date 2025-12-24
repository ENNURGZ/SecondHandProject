using SecondHandProject.Models.Entities;

namespace SecondHandProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task DeleteAsync(string id);
    }
}
