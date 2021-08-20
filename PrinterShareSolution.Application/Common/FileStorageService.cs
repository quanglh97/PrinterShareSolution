using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.Common
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _userContentFolder;
        private readonly string _userUpdateFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private const string UPDATE_FOLDER_NAME = "update-folder";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _userUpdateFolder = Path.Combine(webHostEnvironment.WebRootPath, UPDATE_FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public string GetFileUrlUpdate(string fileName)
        {
            return $"/{UPDATE_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (!Directory.Exists(_userContentFolder))
            {
                Directory.CreateDirectory(_userContentFolder);
            }
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task SaveFileUpdateAsync(Stream mediaBinaryStream, string fileName, string version)
        {
            var versionPath = Path.Combine(_userUpdateFolder, version);  //string.Format("{0:N1}", version
            if (!Directory.Exists(versionPath))
            {
                Directory.CreateDirectory(versionPath);
            }
            var filePath = Path.Combine(versionPath, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            } 
        }

        public async Task DeleteFileUpdateAsync(string fileName)
        {
            var filePath = Path.Combine(_userUpdateFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
