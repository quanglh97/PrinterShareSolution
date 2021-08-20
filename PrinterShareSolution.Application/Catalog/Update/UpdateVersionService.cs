using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PrinterShareSolution.Application.Common;
using PrinterShareSolution.Utilities.Exceptions;
using PrintShareSolution.Data.EF;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PrintShareSolution.ViewModels.Catalog.Update;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using PrintShareSolution.ViewModels.Common;

namespace PrinterShareSolution.Application.Catalog.Update
{
    public class UpdateVersionService : IUpdateVersionService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PrinterShareDbContext _context;
        private readonly IFileStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private const string UPDATE_FOLDER_NAME = "update-folder";

        public UpdateVersionService(
            PrinterShareDbContext context,
            IFileStorageService storageService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        private async Task<string> SaveFileUpdate(IFormFile file, string version)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileUpdateAsync(file.OpenReadStream(), fileName, version);
            return /*"/" + USER_CONTENT_FOLDER_NAME + "/" +*/ fileName;
        }

        public async Task<int> Create(UpdateRequest request)
        {
            var versionList = _context.AppVersionFiles.OrderBy(x => x.Version).Select(x => x.Version).Distinct().ToList();
            if(versionList.Contains(request.version)) throw new PrinterShareException("$this version is exist in database");
            if (request.Files.Count != 0)
            {
                //var query = from avf in _context.AppVersionFiles select new {avf.Version};
                var user = await _userManager.FindByNameAsync(request.adminId);
                if (user == null) throw new PrinterShareException("$this user is invalid");
                else if (request.adminId != "admin") throw new PrinterShareException("$this user is not admin");

                int countFiles = request.Files.Count;
                for(int i=0; i < countFiles; i++)
                {
                    IFormFile file = request.Files[i];
                    string path = request.paths[i];

                    var hash = "";
                    using (var md5 = MD5.Create())
                    {
                        using (var streamReader = new StreamReader(file.OpenReadStream()))
                        {
                            hash = BitConverter.ToString(md5.ComputeHash(streamReader.BaseStream)).Replace("-", "");
                        }
                    }

                    var appVersionFile = new AppVersionFile()
                    {
                        Version = request.version,
                        FileName = file.FileName,
                        FilePath = "\\"+UPDATE_FOLDER_NAME+"\\"+request.version+"\\"+await this.SaveFileUpdate(file, request.version),
                        FilePathSetup = path, 
                        FileSize = file.Length,
                        DateTime = DateTime.Now,
                        Md5 = hash
                    };
                    _context.AppVersionFiles.Add(appVersionFile);
                }
                return await _context.SaveChangesAsync();
            }
            else throw new PrinterShareException($"Cannot update file");
        }

        public async Task<PageUpdateResult<UpdateVM>> GetListFileUpdate(GetListUpdateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.myId);
            if (user == null) throw new PrinterShareException("$this user is invalid");
            var fileNameList = _context.AppVersionFiles.OrderBy(x => x.FileName).Select(x => x.FileName).Distinct().ToList();
            List<UpdateVM> data = new List<UpdateVM>();
            foreach (var fileName in fileNameList)
            {
                var tempQuery = from avf in _context.AppVersionFiles
                select new { avf.FileName, avf.Id,  };
                var maxId = tempQuery.Where(x => x.FileName == fileName).Select(x => x.Id).Max();
                var tempQuery2 = from avf in _context.AppVersionFiles                      
                                 select new {avf};
                var item = tempQuery2.Where(x => x.avf.FileName == fileName && x.avf.Id == maxId).Single();
                UpdateVM updateFile = new UpdateVM()
                {
                    Id = item.avf.Id,
                    Version = item.avf.Version,
                    FileName = item.avf.FileName,
                    FilePath = item.avf.FilePath,
                    FilePathSetup = item.avf.FilePathSetup,
                    FileSize = item.avf.FileSize,
                    Md5 = item.avf.Md5,
                    DateTime = item.avf.DateTime
                };
                data.Add(updateFile);
            }

            //var query = from avf in _context.AppVersionFiles
            //            select new { avf };

            ////filter
            ////query = query.OrderBy(x => x.avf.FileName).Distinct();

            ////3. Paging
            //int totalRow = await query.CountAsync();
            //var data = await query.Select(x => new UpdateVM()
            //    {
            //        Id = x.avf.Id,
            //        Version = x.avf.Version,
            //        FileName = x.avf.FileName,
            //        FilePath = x.avf.FilePath,
            //        FilePathSetup = x.avf.FilePathSetup,
            //        FileSize = x.avf.FileSize,
            //        Md5 = x.avf.Md5,
            //        DateTime = x.avf.DateTime
            //}).ToListAsync();

            //4. Select and projection
            var pagedResult = new PageUpdateResult<UpdateVM>()
            {
                currentVersion = user.CurrentVersion,
                TotalRecords = data.Count,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return pagedResult;
        }

        public async Task<int> CommitUpdate(CommitUpdateRequest request)
        { 
            var user = await _userManager.FindByNameAsync(request.myId);
            if (user == null) throw new PrinterShareException("$this user is invalid");

            user.CurrentVersion = request.version;

            return await _context.SaveChangesAsync();
        }

    }
}   
