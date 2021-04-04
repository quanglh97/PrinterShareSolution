
using PrintShareSolution.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrinterShareSolution.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();
    }
}