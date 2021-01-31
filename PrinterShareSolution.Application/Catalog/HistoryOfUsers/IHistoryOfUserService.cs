using PrintShareSolution.ViewModels.Catalog.HistoryOfUser;
using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.Catalog.HistoryOfUsers
{
    public interface IHistoryOfUserService
    {
        Task<PagedResult<HistoryOfUserVm>> GetByMyId(GetHistoryOfUserPagingRequest request);
        Task<PagedResult<HistoryOfUserVm>> GetByDateRange(GetHistoryOfUserByDateRange request);
        Task<int> RefreshHistory(string MyId);
    }
}
