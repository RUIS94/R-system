using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByNameAsync(string username);
        Task<bool> AddAccountAsync(Account account);
        Task<bool> UpdateBalanceAsync(Account account);
        Task<bool> DeleteAccountAsync(int customerId);
        Task<decimal> GetBalanceByUsernameAsync(string username);
    }
}
