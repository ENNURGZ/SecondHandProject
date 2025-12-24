using SecondHandProject.Models.Entities;
using SecondHandProject.Repositories.Interfaces;
using SecondHandProject.Services.Interfaces;
using SecondHandProject.ViewModels.ProductViewModels;

namespace SecondHandProject.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,
                              ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // LIST ALL PRODUCTS
        public async Task<IEnumerable<ProductListViewModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();

            return products.Select(p => new ProductListViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                CategoryName = p.Category!.Name,
                ImagePath = p.ImagePath ?? "",
                SellerName = p.User!.FullName ?? "Unknown"
            }); 


        }

        // PRODUCTS BY CATEGORY
        public async Task<IEnumerable<ProductListViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryAsync(categoryId);

            return products.Select(p => new ProductListViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                CategoryName = p.Category!.Name,
                ImagePath = p.ImagePath ?? "",
                SellerName = p.User!.FullName ?? "Unknown"   // <-- EKLENECEK SATIR
            });

        }

        // GET EDIT MODEL
        public async Task<ProductEditViewModel> GetEditModelAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            return new ProductEditViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ExistingImagePath = product.ImagePath ?? ""   // null-safe
            };
        }

        // CREATE PRODUCT
        public async Task CreateAsync(ProductCreateViewModel model, string userId)
        {
            string? imageName = null;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/products"
                );

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                imageName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);

                var filePath = Path.Combine(uploadPath, imageName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);
            }   
            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                UserId = userId,
                ImagePath = imageName  
            };

            await _productRepository.AddAsync(product);
        }

        // UPDATE PRODUCT
        public async Task UpdateAsync(ProductEditViewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.Id);
            if (product == null)
                throw new Exception("Product not found");

            
            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/products"
                );

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var imageName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                
                product.ImagePath = imageName;
            }
           

            await _productRepository.UpdateAsync(product);
        }


        // DETAILS PAGE
        public async Task<ProductDetailsViewModel> GetDetailsAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            return new ProductDetailsViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,

                CategoryName = product.Category!.Name,            
                ImagePath = product.ImagePath ?? "",              

                SellerName = product.User!.FullName ?? "Unknown", 
                SellerEmail = product.User.Email ?? "Unknown"     
            };
        }

        // DELETE PRODUCT
        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductListViewModel>> GetProductsByUserAsync(string userId)
        {
            var products = await _productRepository.GetByUserIdAsync(userId);

            return products.Select(p => new ProductListViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                CategoryName = p.Category!.Name,
                ImagePath = p.ImagePath
            });
        }

    }
}
