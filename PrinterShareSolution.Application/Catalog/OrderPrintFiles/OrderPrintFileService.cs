using Microsoft.AspNetCore.Http;
using PrinterShareSolution.Application.Common;
using PrinterShareSolution.Utilities.Exceptions;
using PrintShareSolution.Data.EF;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.Data.Enums;
using PrintShareSolution.ViewModels.Catalog.OrderPrintFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PrintShareSolution.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace PrinterShareSolution.Application.Catalog.OrderPrinterFiles
{
    public class OrderPrintFileService : IOrderPrintFileService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PrinterShareDbContext _context;
        private readonly IFileStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public OrderPrintFileService(
            PrinterShareDbContext context, 
            IFileStorageService storageService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
            //_userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public async Task<int> Create(OrderPrintFileCreateRequest request)
        {
            if (request.ThumbnailFile != null)
            {
                //var printer = await _context.Printers.FindAsync(request.PrinterId);
                //if(printer == null || printer.Status != Status.Active) throw new PrinterShareException($"printer not active: {request.PrinterId}");
                var user = await _userManager.FindByNameAsync(request.MyId);
                if (user == null) throw new PrinterShareException("$this user is invalid");
                
                var orderPrintFile = new OrderPrintFile()
                {
                    UserId = user.Id,
                    PrinterId = request.PrinterId,
                    DateTime = DateTime.Now,
                    FileSize = request.ThumbnailFile.Length,
                    FileName = request.FileName,
                    FilePath = await this.SaveFile(request.ThumbnailFile),
                    Pages = request.Pages,
                    Duplex = (Duplex)request.duplex                    
                };

                _context.OrderPrintFiles.Add(orderPrintFile);
                await _context.SaveChangesAsync();

                //Create History Order Of User
                var query = from lpou in _context.ListPrinterOfUsers
                            where (lpou.PrinterId == request.PrinterId)
                            select new {lpou};
                var userReceiveId = query.Single().lpou.UserId;
                var userReceive = await _context.Users.FindAsync(userReceiveId);
                if (userReceive == null) throw new PrinterShareException($"user receive not active:");

                var historyOfUser = new HistoryOfUser()
                {
                    UserId = user.Id,
                    ReceiveId = userReceive.UserName,
                    PrinterId = request.PrinterId,
                    FileName = request.FileName,
                    FileSize = request.ThumbnailFile.Length,
                    ActionHistory = ActionHistory.OrderPrintFile,
                    DateTime = DateTime.Now,
                    Pages = request.Pages,
                    OrderPrintFileId = orderPrintFile.Id,
                    OrderSendFileId = -1
                    //Result = 
                };
                _context.HistoryOfUsers.Add(historyOfUser);
                await _context.SaveChangesAsync();
                return orderPrintFile.Id;
            }
            else throw new PrinterShareException($"Cannot create OrderPrintFile");
        }

        public async Task<int> Delete(OrderPrintFileDeleteRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.MyId);
            if (user == null) throw new PrinterShareException($"user is invalid: {request.MyId}");
            var orderPrintFile = await _context.OrderPrintFiles.FindAsync(request.Id);
            if (orderPrintFile == null) throw new PrinterShareException($"Cannot find a orderPrintFile : {request.Id}");
            //else if (userId == null) throw new PrinterShareException($"Cannot have user: {request.UserId}");
            else 
            {
                var historyOrders = from hou in _context.HistoryOfUsers
                                   where hou.OrderPrintFileId == request.Id
                                     select(hou);
                foreach(var history in historyOrders)
                {
                    history.Result = (Result)request.Result;
                }
                await _storageService.DeleteFileAsync(orderPrintFile.FilePath);
                _context.OrderPrintFiles.Remove(orderPrintFile); 
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<OrderPrintFileVm>> GetByMyId(GetOrderPrintFilePagingRequest request)
        {
            var UpdateLastRequestUser = await _userManager.FindByNameAsync(request.MyId);
            UpdateLastRequestUser.LastRequestTime = DateTime.Now;
            await _context.SaveChangesAsync();

            //1.Select join
            var query = from opf in _context.OrderPrintFiles
                        join p in _context.Printers on opf.PrinterId equals p.Id
                        join lpou in _context.ListPrinterOfUsers on p.Id equals lpou.PrinterId
                        join u2 in _context.Users on  lpou.UserId equals u2.Id // user dc yeu cau
                        join u1 in _context.Users on opf.UserId equals u1.Id //user gui yeu cau
                        select new { opf, p, lpou, u2, u1 };

            //filter
            query = query.Where(x => x.u2.UserName == request.MyId);

            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new OrderPrintFileVm()
                {
                    Id = x.opf.Id,
                    OrderId = x.u1.UserName,
                    OrderName = x.u1.FullName,
                    Email = x.u1.Email,
                    ReceiveId = x.u2.UserName,
                    ReceiveName =x.u2.FullName,
                    PrinterId = x.p.Id,
                    PrinterName = x.p.Name,
                    FileName = x.opf.FileName,
                    FileSize = x.opf.FileSize,
                    Duplex = (PrintShareSolution.ViewModels.Enums.Duplex)x.opf.Duplex,
                    Pages = x.opf.Pages,
                    DateTime = x.opf.DateTime,
                    FilePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + x.opf.FilePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<OrderPrintFileVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            //Create History DO Order Of User
            var queryHistory = from hou in _context.HistoryOfUsers select new { hou };
            //var orderPrintFiles =  await _context.OrderPrintFiles.Where(i => i.PrinterId == request.PrinterId).ToListAsync();
            foreach (var orderPrintFile in query)
            {
                var instanceHistory = queryHistory.Where(x =>x.hou.OrderPrintFileId == orderPrintFile.opf.Id);
                if (instanceHistory != null) continue;
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = orderPrintFile.lpou.UserId,
                    ReceiveId = orderPrintFile.u2.UserName,
                    PrinterId = orderPrintFile.opf.PrinterId,
                    FileName = orderPrintFile.opf.FileName,
                    ActionHistory = ActionHistory.PrintFile,
                    DateTime = DateTime.Now,
                    Pages = orderPrintFile.opf.Pages,
                    OrderPrintFileId = orderPrintFile.opf.Id,
                    OrderSendFileId = -1,

                };
                _context.HistoryOfUsers.Add(historyOfUser);       
            }
            await _context.SaveChangesAsync();
            return pagedResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return /*"/" + USER_CONTENT_FOLDER_NAME + "/" +*/ fileName;
        }

        public async Task<OrderPrintFileVm> GetById(int id) 
        {
            var orderPrintFile = await _context.OrderPrintFiles.FindAsync(id);
            if(orderPrintFile == null) throw new PrinterShareException($"Printed Id can not found");
            var printer = await _context.Printers.FindAsync(orderPrintFile.PrinterId);
            var u1 = await _context.Users.FindAsync(orderPrintFile.UserId); //user gui yeu cau
            var query = from lpou in _context.ListPrinterOfUsers
                        join u in _context.Users on lpou.UserId equals u.Id
                        select new { lpou, u };
            var u2 = query.Where(x => x.lpou.PrinterId == orderPrintFile.PrinterId).Single(); //user duoc yeu cau
            var orderPrintFileViewModel = new OrderPrintFileVm()
            {
                Id = orderPrintFile.Id,
                OrderId = u1.UserName,
                OrderName = u1.FullName,
                Email = u1.Email,
                ReceiveId = u2.u.UserName,
                ReceiveName = u2.u.FullName,
                PrinterId = orderPrintFile.PrinterId,
                PrinterName = printer.Name,
                FileName = orderPrintFile.FileName,
                DateTime = orderPrintFile.DateTime,
                FileSize = orderPrintFile.FileSize,
                Duplex = (PrintShareSolution.ViewModels.Enums.Duplex)orderPrintFile.Duplex,
                Pages = orderPrintFile.Pages,
                FilePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + orderPrintFile.FilePath
            };
            return orderPrintFileViewModel;
        }


    }
}
