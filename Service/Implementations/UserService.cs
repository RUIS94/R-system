using Business.Interfaces;
using Model.DomainModels;
using Model.DTO;
using Service.Interfaces;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserBusinessRules _userBusiness;

        public UserService(IUserBusinessRules userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userBusiness.GetAllUsersAsync();
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _userBusiness.GetUserByUsernameAsync(username);
        }

        public async Task<bool> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                RoleID = dto.RoleID
            };
            bool success = await _userBusiness.AddUserAsync(user);

            if (success)
            {
                // await _logger.Log("New user created");
                // await _cacheHelper.Invalidate("AllUsers");
            }

            return success;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                RoleID = dto.RoleID
            };
            return await _userBusiness.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            return await _userBusiness.DeleteUserAsync(username);
        }

        public async Task<bool> VerifyUserPasswordAsync(string username, string password)
        {
            return await _userBusiness.ValidatePasswordAsync(username, password);
        }
    }
}