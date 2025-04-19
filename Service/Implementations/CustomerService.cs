using Business.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Model.DomainModels;
using Model.DTO;
using Service.Interfaces;

namespace Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ICustomerBusinessRules _businessRules;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepo,
                           IAccountRepository accountRepo,
                           ICustomerBusinessRules businessRules,
                           IUnitOfWork unitOfWork) 
        {
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
            _businessRules = businessRules;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepo.GetAllCustomersAsync();
        }

        public async Task<bool> CreateCustomerWithAccountAsync(CreateCustomerDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _businessRules.ValidateCustomerAsync(dto);
                var customer = new Customer
                {
                    UserName = dto.UserName,
                    Password = dto.Password,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    VIPlevel = dto.VIPLevel,
                    Notes = dto.Notes
                };
                bool success = await _customerRepo.AddCustomerAsync(customer);
                if (!success)
                    throw new Exception("Failed to create a customer");
                var created = await _customerRepo.GetCustomerByTermAsync(dto.UserName);
                var customerCreated = created.FirstOrDefault();
                if (customerCreated == null)
                    throw new Exception("Cannot find customer ID");
                Account account = new Account
                {
                    CustomerID = customerCreated.ID,
                    Balance = 0
                };
                await _accountRepo.AddAccountAsync(account);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteCustomerWithAccountAsync(string username)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                bool canDelete = await _businessRules.CanDeleteCustomerAsync(username);
                if (!canDelete)
                {
                    throw new InvalidOperationException("Failed to delete customer");
                }
                int id = await  _customerRepo.GetIDBynameAsync(username);
                bool accountDeleted = await _accountRepo.DeleteAccountAsync(id);
                if (!accountDeleted)
                    throw new InvalidOperationException("Failed to delete the customer's account.");
                bool customerDeleted = await _customerRepo.DeleteCustomerAsync(username);
                if (!customerDeleted)
                    throw new InvalidOperationException("Failed to delete the customer.");
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new InvalidOperationException($"Error deleting customer: {ex.Message}");
            }
        }
    }
}