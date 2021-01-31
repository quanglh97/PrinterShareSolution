using Microsoft.EntityFrameworkCore;
using PrinterShareSolution.Application.Common;
using PrinterShareSolution.Utilities.Exceptions;
using PrintShareSolution.Data.EF;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.ViewModels.Catalog.Printers;
using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PrintShareSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace PrinterShareSolution.Application.Catalog.Printers
{
    public class PrinterService :IPrinterService
    {
        private readonly PrinterShareDbContext  _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public PrinterService(
            PrinterShareDbContext context, 
            IStorageService storageService,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _storageService = storageService;
            _userManager = userManager;
        }

        public async Task<int> Create(PrinterCreateRequest request)
        {
            var users = _context.Users;
            var printerOfUser = new List<ListPrinterOfUser>();
            foreach (var user in users)
            {
                if (user.UserName == request.MyId)
                {
                    printerOfUser.Add(new ListPrinterOfUser()
                    {
                        UserId = user.Id,
                    });
                }
            }

            var printer = new Printer()
            {
                 Name = request.Name,
                 Status = (PrintShareSolution.Data.Enums.Status)request.Status,
                 ListPrinterOfUsers = printerOfUser
            };
            _context.Printers.Add(printer);
            await _context.SaveChangesAsync();
            return printer.Id;
        }

        public async Task<int> Update(PrinterUpdateRequest request)
        {
            var printer = await _context.Printers.FindAsync(request.PrinterId);
            var user = await _userManager.FindByNameAsync(request.MyId);
            var printerOfUser = await _context.ListPrinterOfUsers.FirstOrDefaultAsync(
                 x => x.PrinterId == request.PrinterId && x.UserId == user.Id);
            if (printerOfUser == null||printer == null) throw new PrinterShareException($"Cannot Update this printer: {request.PrinterId}");

            printer.Status = (PrintShareSolution.Data.Enums.Status)request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(PrinterDeleteRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.MyId);
            var printer = await _context.Printers.FindAsync(request.PrinterId);
            if (printer == null) throw new PrinterShareException($"Cannot find a printer : {request.PrinterId}");
            else if (user == null) throw new PrinterShareException($"Cannot have user: {request.MyId}");

            var printerOfUser = await _context.ListPrinterOfUsers.FirstOrDefaultAsync(
                x => x.PrinterId == request.PrinterId && x.UserId == user.Id);

            if (printerOfUser == null) throw new PrinterShareException($"This user have not this printer:");
            else _context.Printers.Remove(printer);

            return await _context.SaveChangesAsync();
        }

        public async Task<PrinterVm> GetById(int printerId)
        {
            var query = from p in _context.Printers
                        join pou in _context.ListPrinterOfUsers on p.Id equals pou.PrinterId
                        join u in _context.Users on pou.UserId equals u.Id
                        select new { p, pou, u };

            var printer = query.Where(x => x.p.Id == printerId).Single();
            
            int withOutWarning = await query.CountAsync();

            var printerViewModel = new PrinterVm()
            {
                Id = printer.p.Id,
                myId = printer.u.UserName,
                Name = printer.p.Name,
                FullName = printer.u.FullName,
                Email = printer.u.Email,
                Status = (PrintShareSolution.ViewModels.Enums.Status)printer.p.Status,

            };
            return printerViewModel;
        }

        public async Task<PagedResult<PrinterVm>> GetStatusPaging(GetPrinterPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Printers
                        join pou in _context.ListPrinterOfUsers on p.Id equals pou.PrinterId
                        join u in _context.Users on pou.UserId equals u.Id
                        select new { p, pou, u };

            //Check time to change status of printer.
            var Users = await _context.Users.ToListAsync();
            DateTime now = DateTime.Now;
            foreach (var user in Users)
            {
                TimeSpan span = now.Subtract(user.LastRequestTime);
                if (span.Seconds >= 20)
                {
                    var queryForChangeStatus = from u in _context.Users
                                join lpou in _context.ListPrinterOfUsers on u.Id equals lpou.UserId
                                join p in _context.Printers on lpou.PrinterId equals p.Id
                                where (u.Id == user.Id)
                                select new { u, lpou, p };
                    foreach (var printer in queryForChangeStatus)
                    {
                        printer.p.Status = PrintShareSolution.Data.Enums.Status.InActive;
                    }
                }
            }
            await _context.SaveChangesAsync();
            /////////

            //2. filter
            //where p.Status == (PrintShareSolution.Data.Enums.Status)request.Status
            if ((PrintShareSolution.Data.Enums.Status)request.Status == Status.Active ||
               (PrintShareSolution.Data.Enums.Status)request.Status == Status.InActive)
                query = query.Where(x => x.p.Status == (PrintShareSolution.Data.Enums.Status)request.Status);
            //Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PrinterVm()
                {
                    Id = x.p.Id,
                    myId = x.u.UserName,
                    Name = x.p.Name, 
                    FullName = x.u.FullName,
                    Email = x. u.Email,
                    Status = (PrintShareSolution.ViewModels.Enums.Status)x.p.Status,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<PrinterVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<PrinterVm>> GetKeyWordPaging(string KeyWord)
        {
            //1. Select join
            var query = from p in _context.Printers
                        join lpou in _context.ListPrinterOfUsers on p.Id equals lpou.PrinterId
                        join u in _context.Users on lpou.UserId equals u.Id
                        select new { p, lpou, u };

            //2. filter
            var queryTemp = query;
            if (!string.IsNullOrEmpty(KeyWord))
            {
                query = query.Where(x => x.u.UserName.ToLower().Contains(KeyWord.ToLower()));
                if(query.Count() == 0) 
                {
                    query = queryTemp;
                    query = query.Where(x => x.u.Email.ToLower().Contains(KeyWord.ToLower()));
                }
                if(query.Count() == 0)
                {
                    query = queryTemp;
                    query = query.Where(x => x.u.FullName.ToString().ToLower().Contains(KeyWord.ToLower()));
                }

                ////Check time to change status of printer.
                foreach (var instance in query)
                {
                    DateTime now = DateTime.Now;
                    TimeSpan span = now.Subtract(instance.u.LastRequestTime);
                    if (span.Seconds >= 20)
                    {
                        instance.p.Status = Status.InActive;
                    }
                }
                await _context.SaveChangesAsync();
                query = query.Where(x => x.p.Status == Status.Active);
            }

            

            //Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip(0).Take(10).Select(x => new PrinterVm()
                {
                    Id = x.p.Id,
                    myId = x.u.UserName,
                    Name = x.p.Name,
                    FullName = x.u.FullName,
                    Email = x.u.Email,
                    Status = (PrintShareSolution.ViewModels.Enums.Status)x.p.Status,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<PrinterVm>()
            {
                TotalRecords = totalRow,
                PageSize = 10,
                PageIndex = 1,
                Items = data
            };
            return pagedResult;
        }
    }
}
