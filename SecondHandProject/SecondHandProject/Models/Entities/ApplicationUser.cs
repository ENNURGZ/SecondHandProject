using Microsoft.AspNetCore.Identity;

namespace SecondHandProject.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ProfileImagePath { get; set; }

        // Kullanıcının ürünleri
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
