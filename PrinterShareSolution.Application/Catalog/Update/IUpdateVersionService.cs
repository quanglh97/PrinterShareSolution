using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PrintShareSolution.ViewModels.Catalog.Update;
using PrintShareSolution.ViewModels.Common;

namespace PrinterShareSolution.Application.Catalog.Update
{
    public interface IUpdateVersionService
    {
        Task<int> Create(UpdateRequest request);
        Task<PageUpdateResult<UpdateVM>> GetListFileUpdate(GetListUpdateRequest request);
        Task<int> CommitUpdate(CommitUpdateRequest request);
    }
}
