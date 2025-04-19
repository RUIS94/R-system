using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;

namespace Business.Users
{
    public class UserBusinessRules : IUserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
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

        public async Task<bool> AddUserAsync(User user)
        {
            var existing = await _userRepository.GetUserByTermAsync(user.UserName);
            if (existing.Any())
                throw new InvalidOperationException("Username already exists");
            if (string.IsNullOrEmpty(user.Email) || !user.Email.Contains("@"))
                throw new ArgumentException("Invalid email");
            if (user.Role == null)
                user.Role = new Role { RoleID = 4, Name = "Sales" };

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
                throw new ArgumentException("Username is required");

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
            return storedPassword == inputPassword;
            // return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
        }
    }
}
