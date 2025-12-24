using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SecondHandProject.ViewModels.ProductViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }


        public string? ExistingImagePath { get; set; }

    }
}
