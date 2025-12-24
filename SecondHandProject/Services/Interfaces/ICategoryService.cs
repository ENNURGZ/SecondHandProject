using SecondHandProject.Models.Entities;

namespace SecondHandProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> HasProductsAsync(int categoryId);

        Task<bool> ExistsAsync(int id);
    }
}
