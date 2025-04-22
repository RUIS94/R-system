using Business.Validation;
using Model.DomainModels;
using Model.DTO;

namespace Business.Interfaces
{
    public interface IAccountBusiness
    {
        Task<List<Account>> GetAllAccountsAsync();

        Task<Account> GetAccountByUsernameAsync(string username);

        Task<bool> AddAccountAsync(Account account);

        Task<bool> UpdateBalanceAsync(UpdateBalanceDto dto);
        Task<bool> DeleteAccountAsync(int customerId);

        Task<decimal> GetBalanceByUsernameAsync(string username);
    }
}
