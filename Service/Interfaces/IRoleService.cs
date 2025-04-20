using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}
