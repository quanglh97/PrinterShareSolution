using PrintShareSolution.ViewModels.Catalog.OrderPrintFile;
using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.Catalog.OrderPrinterFiles
{
    public interface IOrderPrintFileService
    {
        Task<int> Create(OrderPrintFileCreateRequest request);
        Task<int> Delete(OrderPrintFileDeleteRequest request);
        Task<PagedResult<OrderPrintFileVm>> GetByPrinterId(GetOrderPrintFilePagingRequest request);
        Task<OrderPrintFileVm> GetById(int id);
    }
}
