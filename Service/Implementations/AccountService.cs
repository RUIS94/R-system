using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionExecutor _transactionExecutor;

        public AccountService(IAccountBusiness accountBusiness, IUnitOfWork unitOfWork)
        {
            _accountBusiness = accountBusiness;
            _unitOfWork = unitOfWork;
            _transactionExecutor = new TransactionExecutor(_unitOfWork);
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountBusiness.GetAllAccountsAsync();
        }

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await _accountBusiness.GetAccountByUsernameAsync(username);
        }

        public async Task<bool> UpdateBalanceAsync(UpdateBalanceDto dto)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _accountBusiness.UpdateBalanceAsync(dto));
        }

        #region
        /// <summary>
        /// These functionalities are currently disabled.
        /// </summary>
        public async Task<bool> AddAccountAsync(Account account)
        {
            return await _accountBusiness.AddAccountAsync(account);
        }

        public async Task<bool> DeleteAccountAsync(int customerId)
        {
            return await _accountBusiness.DeleteAccountAsync(customerId);
        }

        public async Task<decimal> GetBalanceByUsernameAsync(string username)
        {
            return await _accountBusiness.GetBalanceByUsernameAsync(username);
        }
        #endregion
    }
}
