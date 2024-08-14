using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace TLS.Service.Common.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteFileAsync(string fileName, string folderName)
        {
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            var filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }

        }

        public string GetFileUrl(string fileName, string folderName)
        {
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            return Path.Combine(folderPath, fileName);
        }


        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            // Check directory path
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(output);
            }
            return fileName;
        }
    }
}
