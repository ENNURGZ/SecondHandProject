using SecondHandProject.Models.Entities;
using SecondHandProject.Repositories.Implementations;
using SecondHandProject.Repositories.Interfaces;
using SecondHandProject.Services.Interfaces;

namespace SecondHandProject.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
        public async Task<bool> HasProductsAsync(int categoryId)
        {
            return await _productRepository.AnyByCategoryIdAsync(categoryId);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _categoryRepository.ExistsAsync(id);
        }
    }
}
