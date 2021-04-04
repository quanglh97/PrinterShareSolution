using PrintShareSolution.ViewModels.Catalog.OrderSendFile;
using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.Catalog.OrderSendFiles
{
    public interface IOrderSendFileService
    {
        Task<int> Create(OrderSendFileCreateRequest request);
        Task<int> Delete(OrderSendFileDeleteRequest request);
        Task<PagedResult<OrderSendFileVm>> GetByMyId(GetOrderSendFilePagingRequest request);
        Task<OrderSendFileVm> GetById(int id);
        //Task<int> RefreshHistory(string MyId);
    }
}
