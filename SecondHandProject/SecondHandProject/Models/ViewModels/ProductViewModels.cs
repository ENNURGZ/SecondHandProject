namespace SecondHandProject.ViewModels.ProductViewModels
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public string? ImagePath { get; set; }

        public string SellerName { get; set; } = string.Empty;     // Ürünü ekleyen kullanıcı
        public string SellerEmail { get; set; } = string.Empty;    // İletişim için
    }
}
