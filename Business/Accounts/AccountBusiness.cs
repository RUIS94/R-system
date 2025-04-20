using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.Accounts
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountRepository _accountRepository;

        public AccountBusiness(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await _accountRepository.GetAccountByNameAsync(username);
        }

        public async Task<bool> AddAccountAsync(Account account)
        {
            AccountValidator.ValidateAccount(account);
            return await _accountRepository.AddAccountAsync(account);
        }

        public async Task<bool> UpdateBalanceAsync(Account account)
        {
            AccountValidator.ValidateAccount(account);
            return await _accountRepository.UpdateBalanceAsync(account);
        }

        public async Task<bool> DeleteAccountAsync(int customerId)
        {
            return await _accountRepository.DeleteAccountAsync(customerId);
        }

        public async Task<decimal> GetBalanceByUsernameAsync(string username)
        {
            return await _accountRepository.GetBalanceByUsernameAsync(username);
        }
    }
}