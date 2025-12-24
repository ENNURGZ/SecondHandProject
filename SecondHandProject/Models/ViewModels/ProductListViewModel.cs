namespace SecondHandProject.ViewModels.ProductViewModels
{
    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string SellerName { get; set; } = "";
    }
}
