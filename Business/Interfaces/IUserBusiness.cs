using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using Model.DTO;

namespace Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> AddUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(UpdateUserDto dto);
        Task<bool> DeleteUserAsync(string username);
        Task<bool> ValidatePasswordAsync(string username, string inputPassword);
    }
}
