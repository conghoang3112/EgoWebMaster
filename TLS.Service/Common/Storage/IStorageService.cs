using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TLS.Service.Common.Storage
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName, string folderName);

        Task<string> SaveFileAsync(IFormFile file, string folderName);

        Task DeleteFileAsync(string fileName, string folderName);
    }
}
