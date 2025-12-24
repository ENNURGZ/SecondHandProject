using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecondHandProject.Models.Entities;

namespace SecondHandProject.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            // ROLE OLUŞTUR (Admin - User)
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // DEFAULT ADMIN KULLANICISI OLUŞTUR
            string adminEmail = "admin@secondhand.com";
            string adminPassword = "Admin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Admin"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Category Seed
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                   new Category { Name = "Elektronik" },
                    new Category { Name = "Mobilya" },
                    new Category { Name = "Giyim" },
                    new Category { Name = "Kitap" },
                    new Category { Name = "Ev & Yaşam" },
                    new Category { Name = "Spor & Outdoor" }
                );

                await context.SaveChangesAsync();
            }

            // PRODUCT SEED
            if (!context.Products.Any())
            {
                // 🔑 Admin ID'yi DB'den al
                var adminId = await context.Users
                    .Where(u => u.Email == "admin@secondhand.com")
                    .Select(u => u.Id)
                    .FirstAsync();

                var elektronik = await context.Categories.FirstAsync(c => c.Name == "Elektronik");
                var mobilya = await context.Categories.FirstAsync(c => c.Name == "Mobilya");
                var giyim = await context.Categories.FirstAsync(c => c.Name == "Giyim");
                var kitap = await context.Categories.FirstAsync(c => c.Name == "Kitap");
                var ev = await context.Categories.FirstAsync(c => c.Name == "Ev & Yaşam");
                var spor = await context.Categories.FirstAsync(c => c.Name == "Spor & Outdoor");

                var products = new List<Product>
    {
        // ---------------- ELEKTRONİK ----------------
        new Product
        {
            Title = "iPhone 12",
            Description = "64 GB, temiz kullanılmış",
            Price = 25000m,
            CategoryId = elektronik.Id,
            UserId = adminId,
            ImagePath = "iphone12.jpg",
            CreatedAt = DateTime.Now.AddDays(-10)
        },
        new Product
        {
            Title = "Samsung 55\" 4K TV",
            Description = "Smart TV, kutulu",
            Price = 18500m,
            CategoryId = elektronik.Id,
            UserId = adminId,
            ImagePath = "tv.jpg",
            CreatedAt = DateTime.Now.AddDays(-7)
        },

        // ---------------- MOBİLYA ----------------
        new Product
        {
            Title = "Çalışma Masası",
            Description = "Modern, sağlam",
            Price = 1900m,
            CategoryId = mobilya.Id,
            UserId = adminId,
            ImagePath = "masa.jpg",
            CreatedAt = DateTime.Now.AddDays(-14)
        },
        new Product
        {
            Title = "Ofis Sandalyesi",
            Description = "Ergonomik, temiz",
            Price = 1600m,
            CategoryId = mobilya.Id,
            UserId = adminId,
            ImagePath = "sandalye.jpg",
            CreatedAt = DateTime.Now.AddDays(-9)
        },

        // ---------------- GİYİM ----------------
        new Product
        {
            Title = "Kış Montu",
            Description = "M beden, az kullanılmış",
            Price = 1200m,
            CategoryId = giyim.Id,
            UserId = adminId,
            ImagePath = "mont.jpg",
            CreatedAt = DateTime.Now.AddDays(-5)
        },
        new Product
        {
            Title = "Spor Ayakkabı",
            Description = "42 numara",
            Price = 950m,
            CategoryId = giyim.Id,
            UserId = adminId,
            ImagePath = "ayakkabi.jpg",
            CreatedAt = DateTime.Now.AddDays(-3)
        },

        // ---------------- KİTAP ----------------
        new Product
        {
            Title = "Clean Code",
            Description = "Robert C. Martin",
            Price = 350m,
            CategoryId = kitap.Id,
            UserId = adminId,
            ImagePath = "cleancode.jpg",
            CreatedAt = DateTime.Now.AddDays(-20)
        }
    };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

        }
    }
}
