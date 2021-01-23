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

namespace PrinterShareSolution.Application.Catalog.OrderPrinterFiles
{
    public class OrderPrintFileService : IOrderPrintFileService
    {
        private readonly PrinterShareDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public OrderPrintFileService(PrinterShareDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(OrderPrintFileCreateRequest request)
        {
            if (request.ThumbnailFile != null)
            {
                var printer = await _context.Printers.FindAsync(request.PrinterId);
                if(printer == null || printer.Status != Status.Active) throw new PrinterShareException($"printer not active: {request.PrinterId}");
                var orderPrintFile = new OrderPrintFile()
                {
                    UserId = request.UserId,
                    PrinterId = request.PrinterId,
                    ActionOrder = (PrintShareSolution.Data.Enums.ActionOrder)request.ActionOrder,
                    DateTime = DateTime.Now,
                    FileSize = request.ThumbnailFile.Length,
                    FileName = request.FileName,
                    FilePath = await this.SaveFile(request.ThumbnailFile),
                };

                //Create History Order Of User
                var actionOrder = ActionHistory.OrderPrintFile;
                if(request.ActionOrder != PrintShareSolution.ViewModels.Enums.ActionOrder.PrintFile)
                {
                    actionOrder = ActionHistory.OrderSendFile;
                }
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = request.UserId,
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

        public async Task<PagedResult<OrderPrintFileVm>> GetByPrinterId(GetOrderPrintFilePagingRequest request)
        {
            //1.Select join
            var query = from opf in _context.OrderPrintFiles
                        join p in _context.Printers on opf.PrinterId equals p.Id
                        join lpou in _context.ListPrinterOfUsers on p.Id equals lpou.PrinterId
                        where opf.PrinterId == request.PrinterId && lpou.UserId == request.UserId
                        select new { opf, p, lpou };

            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new OrderPrintFileVm()
                {
                    Id = x.opf.Id,
                    UserId = x.lpou.UserId,
                    PrinterId = x.p.Id,
                    FileName = x.opf.FileName,
                    FileSize = x.opf.FileSize,
                    ActionOrder = (PrintShareSolution.ViewModels.Enums.ActionOrder)x.opf.ActionOrder,
                    DateTime = x.opf.DateTime
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

            var orderPrintFiles =  await _context.OrderPrintFiles.Where(i => i.PrinterId == request.PrinterId).ToListAsync();
            foreach (var orderPrintFile in orderPrintFiles)
            {
                var actionDo = ActionHistory.PrintFile;
                if (orderPrintFile.ActionOrder != ActionOrder.PrintFile)
                {
                    actionDo = ActionHistory.ReceiveFile;
                }
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = request.UserId,
                    PrinterId = request.PrinterId,
                    FileName = orderPrintFile.FileName,
                    ActionHistory = actionDo,
                    DateTime = DateTime.Now
                };
                _context.HistoryOfUsers.Add(historyOfUser);
                await _context.SaveChangesAsync();
            }
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

            var orderPrintFileViewModel = new OrderPrintFileVm()
            {
                Id = orderPrintFile.Id,
                UserId = orderPrintFile.UserId,
                PrinterId = orderPrintFile.PrinterId,
                FileName = orderPrintFile.FileName,
                ActionOrder = (PrintShareSolution.ViewModels.Enums.ActionOrder)orderPrintFile.ActionOrder,
                DateTime = orderPrintFile.DateTime,
                FileSize = orderPrintFile.FileSize
            };
            return orderPrintFileViewModel;
        }

        public async Task<int> RefreshHistory(Guid UserId)
        {
            //var userId = await _context.Users.FindAsync(request.UserId);
            var userId = await _context.Users.FindAsync(UserId);
            if (userId == null) throw new PrinterShareException($"This UserId is invalid");
            //else if (userId == null) throw new PrinterShareException($"Cannot have user: {request.UserId}");
            else
            {
                var historyOfUsers = await _context.HistoryOfUsers.ToListAsync();
                DateTime now = DateTime.Now;
                foreach (var historyOfUser in historyOfUsers)
                {
                    TimeSpan span = now.Subtract(historyOfUser.DateTime);
                    if(span.Days >= 0)
                    {
                        _context.HistoryOfUsers.Remove(historyOfUser);
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}
