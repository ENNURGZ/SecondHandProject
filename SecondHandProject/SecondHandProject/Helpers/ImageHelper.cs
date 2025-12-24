using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SecondHandProject.Helpers
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public ImageHelper(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        // Ürün resmi
        public async Task<string> UploadProductImageAsync(IFormFile file)
        {
            var folderPath = _config["AppSettings:ProductImagePath"]!;
            return await UploadFileAsync(file, folderPath);
        }

        // Genel dosya yükleme metodu
        private async Task<string> UploadFileAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string rootPath = _env.WebRootPath;
            string fullFolderPath = Path.Combine(rootPath, folderPath.Replace("wwwroot/", ""));

            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(fullFolderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Veritabanında kaydedilecek path
            return $"{folderPath.Replace("wwwroot/", "")}/{fileName}";
        }

        // Eski resmi sil
        public void DeleteImage(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return;

            string fullPath = Path.Combine(_env.WebRootPath, relativePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
