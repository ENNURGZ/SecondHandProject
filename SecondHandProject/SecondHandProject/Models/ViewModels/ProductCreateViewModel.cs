using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SecondHandProject.ViewModels.ProductViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // Upload edilen dosya
        public IFormFile? ImageFile { get; set; }


    }
}
