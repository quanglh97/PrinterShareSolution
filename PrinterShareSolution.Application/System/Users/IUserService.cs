
using PrintShareSolution.ViewModels.Common;
using PrintShareSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<UserVm>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(string id, UserUpdateRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVm>> GetById(string myId);

        Task<ApiResult<bool>> Delete(string myId);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}