using Business.Interfaces;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;

namespace Business.Accounts
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        public AccountBusiness(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await _accountRepository.GetAccountByNameAsync(username);
        }

        public async Task<bool> UpdateBalanceAsync(UpdateBalanceDto dto)
        {
            AccountValidator.ValidateBalance(dto.Balance);
            int customerId = await _customerRepository.GetIDBynameAsync(dto.UserName);
            var accountToUpdate = new Account
            {
                CustomerID = customerId,
                Balance = dto.Balance
            };
            return await _accountRepository.UpdateBalanceAsync(accountToUpdate);
        }

        #region
        /// <summary>
        /// These functionalities are currently disabled.
        /// </summary>
        public async Task<bool> AddAccountAsync(Account account)
        {
            AccountValidator.ValidateAccount(account);
            return await _accountRepository.AddAccountAsync(account);
        }
        
        public async Task<bool> DeleteAccountAsync(int customerId)
        {
            return await _accountRepository.DeleteAccountAsync(customerId);
        }

        public async Task<decimal> GetBalanceByUsernameAsync(string username)
        {
            return await _accountRepository.GetBalanceByUsernameAsync(username);
        }
        #endregion
    }
}