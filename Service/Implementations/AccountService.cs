using Business.Interfaces;
using Model.DomainModels;
using Service.Interfaces;

namespace Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountBusiness _accountBusiness;

        public AccountService(IAccountBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountBusiness.GetAllAccountsAsync();
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await _accountBusiness.GetAccountByUsernameAsync(username);
        }

        public async Task<bool> AddAccountAsync(Account account)
        {
            return await _accountBusiness.AddAccountAsync(account);
        }

        public async Task<bool> UpdateBalanceAsync(Account account)
        {
            return await _accountBusiness.UpdateBalanceAsync(account);
        }

        public async Task<bool> DeleteAccountAsync(int customerId)
        {
            return await _accountBusiness.DeleteAccountAsync(customerId);
        }

        public async Task<decimal> GetBalanceByUsernameAsync(string username)
        {
            return await _accountBusiness.GetBalanceByUsernameAsync(username);
        }
    }
}
