using System.Text.RegularExpressions;
using Business.Interfaces;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;

namespace Business.Customers
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        public CustomerBusiness(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<List<Customer>> SearchCustomersAsync(string term)
        {
            //if (string.IsNullOrWhiteSpace(term))
            //{
            //    return await _customerRepository.GetAllCustomersAsync();
            //}
            return await _customerRepository.GetCustomerByTermAsync(term);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            await ValidateUpdateCustomerAsync(_customerRepository, customer);
            return await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> UpdateNotesAsync(string username, string notes)
        {
            CommonValidator.ValidateUserName(username);
            return await _customerRepository.UpdateNotesAsync(username, notes);
        }



        public async Task<bool> CreateCustomerWithAccountAsync(CreateCustomerDto dto)
        {
            CustomerValidator.ValidateBasicInfo(dto.UserName, dto.Password, dto.Email);
            await EnsureUsernameNotExistsAsync(_customerRepository, dto.UserName);

            var customer = new Customer
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                VIPlevel = dto.VIPLevel,
                Notes = dto.Notes
            };

            var success = await _customerRepository.AddCustomerAsync(customer);
            if (!success)
                throw new InvalidOperationException("Cannot create customer");

            var created = await _customerRepository.GetCustomerByTermAsync(dto.UserName);
            var customerCreated = created.FirstOrDefault() ?? throw new Exception("Customer not found");

            var account = new Account
            {
                CustomerID = customerCreated.ID,
                Balance = 0
            };

            return await _accountRepository.AddAccountAsync(account);
        }

        public async Task<bool> DeleteCustomerWithAccountAsync(string username)
        {
            await EnsureCustomerCanBeDeletedAsync(_customerRepository, _accountRepository, username);

            int customerId = await _customerRepository.GetIDBynameAsync(username);
            bool accountDeleted = await _accountRepository.DeleteAccountAsync(customerId);
            if (!accountDeleted)
                throw new InvalidOperationException("Account deletion failed.");

            bool customerDeleted = await _customerRepository.DeleteCustomerAsync(username);
            if (!customerDeleted)
                throw new InvalidOperationException("Customer deletion failed.");

            return true;
        }


        private async Task EnsureUsernameNotExistsAsync(ICustomerRepository repo, string username)
        {
            CommonValidator.ValidateUserName(username);
            var existing = await repo.GetCustomerByTermAsync(username);
            if (existing.Any(c => c.UserName?.ToLower() == username.ToLower()))
                throw new InvalidOperationException("Username already exists");
        }
        private async Task EnsureCustomerExistsAsync(ICustomerRepository customerRepository, string username)
        {
            CommonValidator.ValidateUserName(username);
            var existing = await customerRepository.GetCustomerByTermAsync(username);
            if (!existing.Any())
                throw new InvalidOperationException("Customer does not exist");
        }

        private async Task ValidateUpdateCustomerAsync(ICustomerRepository customerRepository, Customer customer)
        {
            //CommonValidator.ValidateUserName(customer.UserName);
            await EnsureCustomerExistsAsync(customerRepository, customer.UserName);
        }

        private async Task EnsureCustomerCanBeDeletedAsync(
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            string username)
        {
            //CommonValidator.ValidateUserName(username);
            await EnsureCustomerExistsAsync(customerRepository, username);

            decimal balance = await accountRepository.GetBalanceByUsernameAsync(username);
            if (balance != 0)
                throw new InvalidOperationException("Cannot delete user with non-zero balance.");
        }
    }
}
