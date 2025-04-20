using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;

namespace Business.Validation
{
    public static class CustomerValidator
    {
        public static void ValidateBasicInfo(string username, string password, string email)
        {
            CommonValidator.ValidateUserName(username);
            CommonValidator.ValidatePassword(password);
            CommonValidator.ValidateEmail(email);
        }
        public static async Task EnsureUsernameNotExistsAsync(ICustomerRepository repo, string username)
        {
            CommonValidator.ValidateUserName(username);
            var existing = await repo.GetCustomerByTermAsync(username);
            if (existing.Any(c => c.UserName?.ToLower() == username.ToLower()))
                throw new InvalidOperationException("Username already exists");
        }
        public static async Task EnsureCustomerExistsAsync(ICustomerRepository customerRepository, string username)
        {
            CommonValidator.ValidateUserName(username);
            var existing = await customerRepository.GetCustomerByTermAsync(username);
            if (!existing.Any())
                throw new InvalidOperationException("Customer does not exist");
        }

        public static async Task ValidateUpdateCustomerAsync(ICustomerRepository customerRepository, Customer customer)
        {
            //CommonValidator.ValidateUserName(customer.UserName);
            await EnsureCustomerExistsAsync(customerRepository, customer.UserName);
        }

        public static async Task EnsureCustomerCanBeDeletedAsync(
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
