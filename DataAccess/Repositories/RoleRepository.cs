using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace DataAccess.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public async Task<List<Role>> GetAllRolesAsync()
        {
            string query = "SELECT id, role_name, role_description FROM roles";
            var table = await ExecuteQueryAsync(query);

            var roles = new List<Role>();
            foreach (DataRow row in table.Rows)
            {
                roles.Add(new Role
                {
                    RoleID = Convert.ToInt32(row["id"]),
                    Name = row["role_name"]?.ToString(),
                    Description = row["role_description"]?.ToString()
                });
            }

            return roles;
        }
    }
}
