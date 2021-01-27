using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrintShareSolution.ViewModels.Catalog.Printers;
using PrintShareSolution.ViewModels.Common;

namespace PrinterShareSolution.Application.Catalog.Printers
{
    public interface IPrinterService
    {
        Task<int> Create(PrinterCreateRequest request);

        Task<int> Update(PrinterUpdateRequest request);

        Task<int> Delete(PrinterDeleteRequest request);

        Task<PrinterVm> GetById(int printerId);

        Task<PagedResult<PrinterVm>> GetStatusPaging(GetPrinterPagingRequest request);
        Task<PagedResult<PrinterVm>> GetKeyWordPaging(string KeyWord);
    }
}
