using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface IRoleBusiness
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}
