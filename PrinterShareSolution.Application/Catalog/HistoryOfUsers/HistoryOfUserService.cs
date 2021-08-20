using Microsoft.AspNetCore.Identity;
using PrinterShareSolution.Application.Common;
using PrintShareSolution.Data.EF;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.ViewModels.Catalog.HistoryOfUser;
using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PrinterShareSolution.Utilities.Exceptions;

namespace PrinterShareSolution.Application.Catalog.HistoryOfUsers
{
    public class HistoryOfUserService : IHistoryOfUserService
    {
        private readonly PrinterShareDbContext _context;
        private readonly IFileStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public HistoryOfUserService(
            PrinterShareDbContext context,
            IFileStorageService storageService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }
        public async Task<PagedResult<HistoryOfUserVm>> GetByMyId(GetHistoryOfUserPagingRequest request)
        {
            //clean History
            var historyOfUsers = await _context.HistoryOfUsers.ToListAsync();
            DateTime now = DateTime.Now;
            foreach (var historyOfUser in historyOfUsers)
            {
                TimeSpan span = now.Subtract(historyOfUser.DateTime);
                if (span.Days >= 10)
                {
                    _context.HistoryOfUsers.Remove(historyOfUser);
                }
            }
            await _context.SaveChangesAsync();

            //1. Select join
            var queryPrint = from hou in _context.HistoryOfUsers
                        join u1 in _context.Users on hou.UserId equals u1.Id  // user gui yeu cau
                        join u2 in _context.Users on hou.ReceiveId equals u2.UserName //user nhan yeu cau
                        //join opf in _context.OrderPrintFiles on hou.OrderPrintFileId equals opf.Id
                        join p in _context.Printers on hou.PrinterId equals p.Id                      
                        select new { hou, u1, u2, p};
            var querySend = from hou in _context.HistoryOfUsers
                            join u1 in _context.Users on hou.UserId equals u1.Id  // user gui yeu cau
                            join u2 in _context.Users on hou.ReceiveId equals u2.UserName //user nhan yeu cau
                            //join osf in _context.OrderSendFiles on hou.OrderSendFileId equals osf.Id
                            select new { hou, u1, u2 };

            //2. filter
            //where p.Status == (PrintShareSolution.Data.Enums.Status)request.Status
            queryPrint = queryPrint.Where(x => x.u1.UserName == request.MyId || x.u2.UserName == request.MyId);
            queryPrint = queryPrint.Where(x => x.hou.PrinterId != -1);
            //queryPrint = queryPrint.Where(x => x.hou.OrderPrintFileId == x.opf.Id);

            querySend = querySend.Where(x => x.u1.UserName == request.MyId || x.u2.UserName == request.MyId);
            querySend = querySend.Where(x=>x.hou.PrinterId == -1);
            //querySend = querySend.Where(x => x.hou.OrderSendFileId == x.osf.Id);
            //Paging
            int totalRow = await queryPrint.CountAsync();
            totalRow += await querySend.CountAsync();

            var dataPrint = await queryPrint.Skip(0)
                .Take(1000).Select(x => new HistoryOfUserVm()
                {
                    Id = x.hou.Id,
                    OrderId = x.u1.UserName,
                    OrderName = x.u1.FullName,
                    ReceiveId = x.u2.UserName,
                    ReceiveName =x.u2.FullName,
                    PrinterId = x.hou.PrinterId,
                    OrderPrintFileId = x.hou.OrderPrintFileId,
                    OrderSendFileId = x.hou.OrderSendFileId,
                    PrinterName = x.p.Name,
                    FileName = x.hou.FileName,
                    FileSize = x.hou.FileSize,
                    ActionHistory = (PrintShareSolution.ViewModels.Enums.ActionHistory)x.hou.ActionHistory,
                    Pages = x.hou.Pages,
                    Result = (PrintShareSolution.ViewModels.Enums.Result)x.hou.Result,
                    DateTime = x.hou.DateTime
                }).ToListAsync();

            var dataSend = await querySend.Skip(0).Take(1000).Select(x => new HistoryOfUserVm()
            {
                Id = x.hou.Id,
                OrderId = x.u1.UserName,
                OrderName = x.u1.FullName,
                ReceiveId = x.u2.UserName,
                ReceiveName = x.u2.FullName,
                PrinterId = x.hou.PrinterId,
                OrderPrintFileId = x.hou.OrderPrintFileId,
                OrderSendFileId = x.hou.OrderSendFileId,
                PrinterName = null,
                FileName = x.hou.FileName,
                FileSize = x.hou.FileSize,
                ActionHistory = (PrintShareSolution.ViewModels.Enums.ActionHistory)x.hou.ActionHistory,
                Pages = x.hou.Pages,
                Result = (PrintShareSolution.ViewModels.Enums.Result)x.hou.Result,
                DateTime = x.hou.DateTime
            }).ToListAsync();

            var data = dataPrint;
            data.AddRange(dataSend);
            //4. Select and projection
            var pagedResult = new PagedResult<HistoryOfUserVm>()
            {
                TotalRecords = totalRow,
                PageSize = 1000,
                PageIndex = 1,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<HistoryOfUserVm>> GetByDateRange(GetHistoryOfUserByDateRange request)
        {
            //1. Select join
            var queryPrint = from hou in _context.HistoryOfUsers
                             join u1 in _context.Users on hou.UserId equals u1.Id  // user gui yeu cau
                             join u2 in _context.Users on hou.ReceiveId equals u2.UserName //user nhan yeu cau
                             //join opf in _context.OrderPrintFiles on hou.OrderPrintFileId equals opf.Id
                             join p in _context.Printers on hou.PrinterId equals p.Id
                             select new { hou, u1, u2, p };
            var querySend = from hou in _context.HistoryOfUsers
                            join u1 in _context.Users on hou.UserId equals u1.Id  // user gui yeu cau
                            join u2 in _context.Users on hou.ReceiveId equals u2.UserName //user nhan yeu cau
                            //join osf in _context.OrderSendFiles on hou.OrderSendFileId equals osf.Id
                            select new { hou, u1, u2 };

            //2. filter
            queryPrint = queryPrint.Where(x => x.u1.UserName == request.MyId || x.u2.UserName == request.MyId);
            queryPrint = queryPrint.Where(x => x.hou.DateTime >= request.FirstDay && x.hou.DateTime <= request.LastDay);

            querySend = querySend.Where(x => x.u1.UserName == request.MyId || x.u2.UserName == request.MyId);
            querySend = querySend.Where(x => x.hou.DateTime >= request.FirstDay && x.hou.DateTime <= request.LastDay);
            //Paging
            int totalRow = await queryPrint.CountAsync();

            var dataPrint = await queryPrint.Select(x => new HistoryOfUserVm()
            {
                Id = x.hou.Id,
                OrderId = x.u1.UserName,
                OrderName = x.u1.FullName,
                ReceiveId = x.u2.UserName,
                ReceiveName = x.u2.FullName,
                PrinterId = x.hou.PrinterId,
                OrderPrintFileId = x.hou.OrderPrintFileId,
                OrderSendFileId = x.hou.OrderSendFileId,
                PrinterName =x.p.Name,
                FileName = x.hou.FileName,
                FileSize = x.hou.FileSize,
                ActionHistory = (PrintShareSolution.ViewModels.Enums.ActionHistory)x.hou.ActionHistory,
                Pages = x.hou.Pages,
                Result = (PrintShareSolution.ViewModels.Enums.Result)x.hou.Result,
                DateTime = x.hou.DateTime
            }).ToListAsync();

            var dataSend = await querySend.Select(x => new HistoryOfUserVm()
            {
                Id = x.hou.Id,
                OrderId = x.u1.UserName,
                OrderName = x.u1.FullName,
                ReceiveId = x.u2.UserName,
                ReceiveName = x.u2.FullName,
                PrinterId = x.hou.PrinterId,
                OrderPrintFileId = x.hou.OrderPrintFileId,
                OrderSendFileId = x.hou.OrderSendFileId,
                PrinterName = null,
                FileName = x.hou.FileName,
                FileSize = x.hou.FileSize,
                ActionHistory = (PrintShareSolution.ViewModels.Enums.ActionHistory)x.hou.ActionHistory,
                Pages = x.hou.Pages,
                Result = (PrintShareSolution.ViewModels.Enums.Result)x.hou.Result,
                DateTime = x.hou.DateTime
            }).ToListAsync();

            var data = dataPrint;
            data.AddRange(dataSend);

            //4. Select and projection
            var pagedResult = new PagedResult<HistoryOfUserVm>()
            {
                TotalRecords = totalRow,
                PageSize = 1000,
                PageIndex = 1,
                Items = data
            };
            return pagedResult;
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
                    if (span.Days >= 10)
                    {
                        _context.HistoryOfUsers.Remove(historyOfUser);
                    }
                }
            }

            return await _context.SaveChangesAsync();
        }


    }
}
