namespace SecondHandProject.ViewModels.Profile
{
    public class ProfileIndexVm
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImagePath { get; set; }

        public List<MyProductVm> Products { get; set; } = new();
    }

    public class MyProductVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
