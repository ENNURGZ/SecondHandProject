using SecondHandProject.Models.Entities;

namespace SecondHandProject.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<bool> AnyByCategoryIdAsync(int categoryId);

        Task<IEnumerable<Product>> GetByUserIdAsync(string userId);

    }
}
