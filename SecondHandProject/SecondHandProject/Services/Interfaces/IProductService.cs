using SecondHandProject.ViewModels.ProductViewModels;

namespace SecondHandProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>> GetAllAsync();
        Task<IEnumerable<ProductListViewModel>> GetProductsByCategoryAsync(int categoryId);

        Task<ProductEditViewModel> GetEditModelAsync(int id);
        Task CreateAsync(ProductCreateViewModel model, string userId);
        Task UpdateAsync(ProductEditViewModel model);
        Task<ProductDetailsViewModel> GetDetailsAsync(int id);
        Task<IEnumerable<ProductListViewModel>> GetProductsByUserAsync(string userId);


        Task DeleteAsync(int id);
    }
}
