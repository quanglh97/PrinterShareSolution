using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.Common
{
    public interface IFileStorageService
    {
        string GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task SaveFileUpdateAsync(Stream mediaBinaryStream, string fileName, string version);

        Task DeleteFileAsync(string fileName);
        Task DeleteFileUpdateAsync(string fileName);
    }
}
