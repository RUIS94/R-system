using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetUserByTermAsync(string searchTerm);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string username);
        Task<string?> GetPasswordByUsernameAsync(string username);
        Task<int?> GetRoleByUsernameAsync(string username);
    }
}
