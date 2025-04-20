using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Model.DomainModels;
using Service.Interfaces;

namespace Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleBusiness _roleBusiness;

        public RoleService(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleBusiness.GetAllRolesAsync();
        }
    }
}
