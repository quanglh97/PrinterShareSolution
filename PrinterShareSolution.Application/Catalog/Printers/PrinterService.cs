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

namespace PrinterShareSolution.Application.Catalog.Printers
{
    public class PrinterService :IPrinterService
    {
        private readonly PrinterShareDbContext  _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public PrinterService(PrinterShareDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(PrinterCreateRequest request)
        {
            var users = _context.Users;
            var printerOfUser = new List<ListPrinterOfUser>();
            foreach (var user in users)
            {
                if (user.Id == request.UserId)
                {
                    printerOfUser.Add(new ListPrinterOfUser()
                    {
                        UserId = request.UserId,
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
            var printerOfUser = await _context.ListPrinterOfUsers.FirstOrDefaultAsync(
                 x => x.PrinterId == request.PrinterId && x.UserId == request.UserId);
            if (printerOfUser == null||printer == null) throw new PrinterShareException($"Cannot Update this printer: {request.PrinterId}");

            printer.Status = (PrintShareSolution.Data.Enums.Status)request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(PrinterDeleteRequest request)
        {
            var userId = await _context.Users.FindAsync(request.UserId);
            var printer = await _context.Printers.FindAsync(request.PrinterId);
            if (printer == null) throw new PrinterShareException($"Cannot find a printer : {request.PrinterId}");
            else if (userId == null) throw new PrinterShareException($"Cannot have user: {request.UserId}");

            var printerOfUser = await _context.ListPrinterOfUsers.FirstOrDefaultAsync(
                x => x.PrinterId == request.PrinterId && x.UserId == request.UserId);

            if (printerOfUser == null) throw new PrinterShareException($"This user have not this printer:");
            else _context.Printers.Remove(printer);

            return await _context.SaveChangesAsync();
        }

        public async Task<PrinterVm> GetById(int printerId, Guid userId)
        {
            var printer = await _context.Printers.FindAsync(printerId);
            var printerOfUser = await _context.ListPrinterOfUsers.FirstOrDefaultAsync(x => x.PrinterId == printerId
            && x.UserId == userId);

            var printerViewModel = new PrinterVm()
            {
                Id = printer.Id,
                UserId = printerOfUser.UserId,
                Name = printer.Name,
                Status = (PrintShareSolution.ViewModels.Enums.Status)printer.Status,

            };
            return printerViewModel;
        }

        public async Task<PagedResult<PrinterVm>> GetAllPaging(GetPrinterPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Printers 
                        join pou in _context.ListPrinterOfUsers on p.Id equals pou.PrinterId
                        select new { p, pou };

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
                    UserId = x.pou.UserId,
                    Name = x.p.Name, 
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
    }
}
