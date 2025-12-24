namespace SecondHandProject.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        // Tarih
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
