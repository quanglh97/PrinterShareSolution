using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PrinterShareSolution.Application.Common;
using PrinterShareSolution.Utilities.Exceptions;
using PrintShareSolution.Data.EF;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.ViewModels.Catalog.OrderSendFile;
using PrintShareSolution.ViewModels.Common;
using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PrinterShareSolution.Application.Catalog.OrderSendFiles
{
    public class OrderSendFileService : IOrderSendFileService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly PrinterShareDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public OrderSendFileService(
            PrinterShareDbContext context,
            IStorageService storageService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return /*"/" + USER_CONTENT_FOLDER_NAME + "/" +*/ fileName;
        }

        public async Task<int> Create(OrderSendFileCreateRequest request)
        {
            if (request.ThumbnailFile != null)
            {
                var userReceive = await _userManager.FindByNameAsync(request.UserReceive);
                if (userReceive == null) throw new PrinterShareException($"have not user receive: {request.UserReceive}");
                var user = await _userManager.FindByNameAsync(request.MyId);
                if (user == null) throw new PrinterShareException("$this user is invalid");
                var orderSendFile = new OrderSendFile()
                {
                    UserId = user.Id,
                    ReceiveId = request.UserReceive,
                    DateTime = DateTime.Now,
                    FileSize = request.ThumbnailFile.Length,
                    FileName = request.FileName,
                    FilePath = await this.SaveFile(request.ThumbnailFile),
                };
                _context.OrderSendFiles.Add(orderSendFile);
                await _context.SaveChangesAsync();

                //Create History Order Of User
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = user.Id,
                    PrinterId = -1,
                    ReceiveId = request.UserReceive,
                    FileName = request.FileName,
                    ActionHistory = (PrintShareSolution.Data.Enums.ActionHistory)ActionHistory.OrderSendFile,
                    DateTime = DateTime.Now,
                    Pages = 0,
                    OrderPrintFileId = -1,
                    OrderSendFileId = orderSendFile.Id,
                    //Result=
                };
                _context.HistoryOfUsers.Add(historyOfUser);
                await _context.SaveChangesAsync();
                return orderSendFile.Id;
            }
            else throw new PrinterShareException($"Cannot create OrderSendFile");
        }

        public async Task<int> Delete(OrderSendFileDeleteRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.MyId);
            if (user == null) throw new PrinterShareException($"user is invalid : {request.MyId}");
            var orderSendFile = await _context.OrderSendFiles.FindAsync(request.Id);
            if (orderSendFile == null) throw new PrinterShareException($"Cannot find a orderSendFile : {request.Id}");
            else
            {
                var historyOrders = from hou in _context.HistoryOfUsers
                                    where hou.OrderSendFileId == request.Id
                                    select (hou);
                foreach (var history in historyOrders)
                {
                    history.Result = (PrintShareSolution.Data.Enums.Result)request.Result;
                }
                await _storageService.DeleteFileAsync(orderSendFile.FilePath);
                _context.OrderSendFiles.Remove(orderSendFile);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<OrderSendFileVm> GetById(int id)
        {
            var orderSendFile = await _context.OrderSendFiles.FindAsync(id);
            if (orderSendFile == null) throw new PrinterShareException($"this order can not found");
            var user = await _context.Users.FindAsync(orderSendFile.UserId);
            var receiveUser = await _userManager.FindByNameAsync(orderSendFile.ReceiveId);
            var orderSendFileViewModel = new OrderSendFileVm()
            {
                Id = orderSendFile.Id,
                OrderId = user.UserName,
                OrderName = user.FullName,
                Email = user.Email,
                ReceiveId = orderSendFile.ReceiveId,
                ReceiveName = receiveUser.FullName,
                FileName = orderSendFile.FileName,
                DateTime = orderSendFile.DateTime,
                FileSize = orderSendFile.FileSize,
                FilePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + orderSendFile.FilePath
            };
            return orderSendFileViewModel;
        }

        public async Task<PagedResult<OrderSendFileVm>> GetByMyId(GetOrderSendFilePagingRequest request)
        {
/*            var UpdateLastRequestUser = await _userManager.FindByNameAsync(request.MyId);
            UpdateLastRequestUser.LastRequestTime = DateTime.Now;*/

            //1.Select join
            var query = from osf in _context.OrderSendFiles
                        join u in _context.Users on osf.UserId equals u.Id
                        select new { osf, u };

            //filter
            query = query.Where(x => x.osf.ReceiveId == request.MyId);
            var receiveUser = await _userManager.FindByNameAsync(request.MyId);
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new OrderSendFileVm()
                {
                    Id = x.osf.Id,
                    OrderId = x.u.UserName,
                    OrderName = x.u.FullName,
                    Email = x.u.Email,
                    ReceiveId = x.osf.ReceiveId,
                    ReceiveName = receiveUser.FullName,
                    FileName = x.osf.FileName,
                    FileSize = x.osf.FileSize,
                    DateTime = x.osf.DateTime,
                    FilePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + x.osf.FilePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<OrderSendFileVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            //Create History DO Order Of User

            //var orderPrintFiles =  await _context.OrderPrintFiles.Where(i => i.PrinterId == request.PrinterId).ToListAsync();
            foreach (var orderSendFile in query)
            {
                var historyOfUser = new HistoryOfUser()
                {
                    UserId = orderSendFile.osf.UserId,
                    ReceiveId = orderSendFile.osf.ReceiveId,
                    PrinterId = -1,
                    FileName = orderSendFile.osf.FileName,
                    ActionHistory = (PrintShareSolution.Data.Enums.ActionHistory)ActionHistory.ReceiveFile,
                    DateTime = DateTime.Now,
                    OrderPrintFileId = -1,
                    OrderSendFileId = orderSendFile.osf.Id,
                    //Result = 
                };
                _context.HistoryOfUsers.Add(historyOfUser);
            }
            await _context.SaveChangesAsync();
            return pagedResult;
        }
    }
}
