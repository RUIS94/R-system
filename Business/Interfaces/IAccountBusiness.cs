using Business.Validation;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface IAccountBusiness
    {
        Task<List<Account>> GetAllAccountsAsync();

        Task<Account> GetAccountByUsernameAsync(string username);

        Task<bool> AddAccountAsync(Account account);

        Task<bool> UpdateBalanceAsync(Account account);
        Task<bool> DeleteAccountAsync(int customerId);

        Task<decimal> GetBalanceByUsernameAsync(string username);
    }
}
