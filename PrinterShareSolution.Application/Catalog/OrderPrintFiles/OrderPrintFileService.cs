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
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public OrderPrintFileService(
            PrinterShareDbContext context, 
            IStorageService storageService,
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
                var printer = await _context.Printers.FindAsync(request.PrinterId);
                if(printer == null || printer.Status != Status.Active) throw new PrinterShareException($"printer not active: {request.PrinterId}");
                var user = await _userManager.FindByNameAsync(request.MyId);
                if (user == null) throw new PrinterShareException("$this user is invalid");
                var orderPrintFile = new OrderPrintFile()
                {
                    UserId = user.Id,
                    PrinterId = request.PrinterId,
                    ActionOrder = (PrintShareSolution.Data.Enums.ActionOrder)request.ActionOrder,
                    DateTime = DateTime.Now,
                    FileSize = request.ThumbnailFile.Length,
                    FileName = request.FileName,
                    FilePath = await this.SaveFile(request.ThumbnailFile),
                };

                //Create History Order Of User
                var actionOrder = ActionHistory.OrderPrintFile;
                if (request.ActionOrder == PrintShareSolution.ViewModels.Enums.ActionOrder.PrintFile)
                {
                    actionOrder = ActionHistory.OrderPrintFile;
                }
                else
                    actionOrder = ActionHistory.OrderSendFile;
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = user.Id,
                    PrinterId = request.PrinterId,
                    FileName = request.FileName,
                    ActionHistory = actionOrder,
                    DateTime = DateTime.Now
                };
                _context.HistoryOfUsers.Add(historyOfUser);

                _context.OrderPrintFiles.Add(orderPrintFile);
                await _context.SaveChangesAsync();
                return orderPrintFile.Id;
            }
            else throw new PrinterShareException($"Cannot create OrderPrintFile");
        }

        public async Task<int> Delete(OrderPrintFileDeleteRequest request)
        {
            //var userId = await _context.Users.FindAsync(request.UserId);
            var orderPrintFile = await _context.OrderPrintFiles.FindAsync(request.Id);
            if (orderPrintFile == null) throw new PrinterShareException($"Cannot find a orderPrintFile : {request.Id}");
            //else if (userId == null) throw new PrinterShareException($"Cannot have user: {request.UserId}");
            else 
            { 
                await _storageService.DeleteFileAsync(orderPrintFile.FilePath);
                _context.OrderPrintFiles.Remove(orderPrintFile); 
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<OrderPrintFileVm>> GetByMyId(GetOrderPrintFilePagingRequest request)
        {
            //1.Select join
            var query = from opf in _context.OrderPrintFiles
                        join p in _context.Printers on opf.PrinterId equals p.Id
                        join lpou in _context.ListPrinterOfUsers on p.Id equals lpou.PrinterId
                        join u in _context.Users on  lpou.UserId equals u.Id
                        select new { opf, p, lpou, u };

            //filter
            query = query.Where(x => x.u.UserName == request.MyId);

            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new OrderPrintFileVm()
                {
                    Id = x.opf.Id,
                    UserId = x.lpou.UserId,
                    PrinterId = x.p.Id,
                    PrinterName = x.p.Name,
                    FileName = x.opf.FileName,
                    FileSize = x.opf.FileSize,
                    ActionOrder = (PrintShareSolution.ViewModels.Enums.ActionOrder)x.opf.ActionOrder,
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

            //var orderPrintFiles =  await _context.OrderPrintFiles.Where(i => i.PrinterId == request.PrinterId).ToListAsync();
            foreach (var orderPrintFile in query)
            {
                var actionDo = ActionHistory.PrintFile;
                if (orderPrintFile.opf.ActionOrder != ActionOrder.PrintFile)
                {
                    actionDo = ActionHistory.ReceiveFile;
                }
                else actionDo = ActionHistory.PrintFile;
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = orderPrintFile.lpou.UserId,
                    PrinterId = orderPrintFile.opf.PrinterId,
                    FileName = orderPrintFile.opf.FileName,
                    ActionHistory = actionDo,
                    DateTime = DateTime.Now
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
            var orderPrintFileViewModel = new OrderPrintFileVm()
            {
                Id = orderPrintFile.Id,
                UserId = orderPrintFile.UserId,
                PrinterId = orderPrintFile.PrinterId,
                PrinterName = printer.Name,
                FileName = orderPrintFile.FileName,
                ActionOrder = (PrintShareSolution.ViewModels.Enums.ActionOrder)orderPrintFile.ActionOrder,
                DateTime = orderPrintFile.DateTime,
                FileSize = orderPrintFile.FileSize,
                FilePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + orderPrintFile.FilePath
        };
            return orderPrintFileViewModel;
        }

        public async Task<int> RefreshHistory(string MyId)
        {
            //var userId = await _context.Users.FindAsync(request.UserId);
            var user = await _userManager.FindByNameAsync(MyId);
            if (user == null) throw new PrinterShareException($"This Your Id is invalid");
            //else if (userId == null) throw new PrinterShareException($"Cannot have user: {request.UserId}");
            else
            {
                var historyOfUsers = await _context.HistoryOfUsers.ToListAsync();
                DateTime now = DateTime.Now;
                foreach (var historyOfUser in historyOfUsers)
                {
                    TimeSpan span = now.Subtract(historyOfUser.DateTime);
                    if(span.Days >= 10)
                    {
                        _context.HistoryOfUsers.Remove(historyOfUser);
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}
