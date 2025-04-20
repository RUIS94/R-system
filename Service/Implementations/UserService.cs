using Business.Interfaces;
using Model.DomainModels;
using Model.DTO;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserBusiness _userBusiness;
        private readonly TransactionExecutor _transactionExecutor;

        public UserService(IUserBusiness userBusiness, TransactionExecutor transactionExecutor)
        {
            _userBusiness = userBusiness;
            _transactionExecutor = transactionExecutor;
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
            var user = _userBusiness.CreateUserFromDto(dto);

            return await _transactionExecutor.ExecuteTransactionAsync(async () =>
            {
                bool success = await _userBusiness.AddUserAsync(user);
                if (success)
                {
                    // Add logging, cache invalidation if needed
                }
                return success;
            });
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = _userBusiness.UpdateUserFromDto(dto);

            return await _transactionExecutor.ExecuteTransactionAsync(async () =>
            {
                return await _userBusiness.UpdateUserAsync(user);
            });
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            return await _transactionExecutor.ExecuteTransactionAsync(async () =>
            {
                return await _userBusiness.DeleteUserAsync(username);
            });
        }


        public async Task<bool> VerifyUserPasswordAsync(string username, string password)
        {
            return await _userBusiness.ValidatePasswordAsync(username, password);
        }
    }
}