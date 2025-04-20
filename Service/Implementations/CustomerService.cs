using Business.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Model.DomainModels;
using Model.DTO;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerBusiness _customerBusiness;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionExecutor _transactionExecutor;

        public CustomerService(ICustomerBusiness businessRules, IUnitOfWork unitOfWork)
        {
            _customerBusiness = businessRules;
            _unitOfWork = unitOfWork;
            _transactionExecutor = new TransactionExecutor(_unitOfWork);
        }


        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerBusiness.GetAllCustomersAsync();
        }
        public async Task<List<Customer>> SearchCustomersAsync(string term)
        {
            return await _customerBusiness.SearchCustomersAsync(term);
        }
        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _customerBusiness.UpdateCustomerAsync(customer));
        }

        public async Task<bool> UpdateNotesAsync(string username, string notes)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _customerBusiness.UpdateNotesAsync(username, notes));
        }

        public async Task<bool> CreateCustomerWithAccountAsync(CreateCustomerDto dto)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _customerBusiness.CreateCustomerWithAccountAsync(dto));
        }

        public async Task<bool> DeleteCustomerWithAccountAsync(string username)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _customerBusiness.DeleteCustomerWithAccountAsync(username),
                "Delete failed");
        }
    }
}