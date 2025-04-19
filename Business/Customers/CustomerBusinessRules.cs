using System.Text.RegularExpressions;
using Business.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Model.DomainModels;
using Model.DTO;

namespace Business.Customers
{
    public class CustomerBusinessRules : ICustomerBusinessRules
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        public CustomerBusinessRules(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        private async Task<bool> IsUsernameTakenAsync(string username)
        {
            var customers = await _customerRepository.GetCustomerByTermAsync(username);
            return customers.Any(c => c.UserName == username);
        }


        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^\S+@\S+\.\S+$");
        }

        public async Task ValidateCustomerAsync(CreateCustomerDto customer)
        {
            if (string.IsNullOrWhiteSpace(customer.UserName))
                throw new ArgumentException("Username cannot be null");

            if (!IsValidEmail(customer.Email))
                throw new ArgumentException("The email format is incorrect");

            if (await IsUsernameTakenAsync(customer.UserName))
                throw new InvalidOperationException("Username already exists");
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<List<Customer>> SearchCustomersAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return await _customerRepository.GetAllCustomersAsync();
            }
            return await _customerRepository.GetCustomerByTermAsync(term);
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.UserName) ||
                string.IsNullOrWhiteSpace(customer.Password) ||
                string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Missing required fields");
            }
            var existing = await _customerRepository.GetCustomerByTermAsync(customer.UserName);
            if (existing.Any(c => c.UserName?.ToLower() == customer.UserName.ToLower()))
            {
                throw new InvalidOperationException("Customer with this username already exists");
            }
            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.UserName))
                throw new ArgumentException("Username is required");
            var existing = await _customerRepository.GetCustomerByTermAsync(customer.UserName);
            if (!existing.Any())
                throw new InvalidOperationException("Customer does not exist");
            return await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> CanDeleteCustomerAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required");
            var existing = await _customerRepository.GetCustomerByTermAsync(username);
            if (!existing.Any())
                throw new InvalidOperationException("Customer does not exist");
            decimal balance = await _accountRepository.GetBalanceByUsernameAsync(username);
            if (balance != 0)
            {
                throw new InvalidOperationException("Cannot delete user with non-zero balance.");
            }
            return true; 
        }

        public async Task<bool> DeleteCustomerAsync(string username)
        {
            return await _customerRepository.DeleteCustomerAsync(username);
        }

        public async Task<bool> UpdateNotesAsync(string username, string notes)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required");
            return await _customerRepository.UpdateNotesAsync(username, notes);
        }
    }
}
