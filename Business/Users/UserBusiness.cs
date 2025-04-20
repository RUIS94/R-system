using Business.Interfaces;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;

namespace Business.Users
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var users = await _userRepository.GetUserByTermAsync(username);
            return users.FirstOrDefault();
        }

        public async Task<bool> AddUserAsync(CreateUserDto dto)
        {
            UserValidator.ValidateUser(dto);

            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                RoleID = dto.RoleID
            };

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto dto)
        {
            UserValidator.ValidateUser(dto);

            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                RoleID = dto.RoleID
            };

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            int? roleID = await _userRepository.GetRoleByUsernameAsync(username);
            if (roleID == 1)
                throw new InvalidOperationException("Cannot delete admin user");

            return await _userRepository.DeleteUserAsync(username);
        }

        public async Task<bool> ValidatePasswordAsync(string username, string inputPassword)
        {
            var storedPassword = await _userRepository.GetPasswordByUsernameAsync(username);

            if (storedPassword == null)
            {
                return false;
            }

            // In a real application, you would use a proper hashing algorithm for password verification
            // return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
            return storedPassword == inputPassword;
        }
    }
}
