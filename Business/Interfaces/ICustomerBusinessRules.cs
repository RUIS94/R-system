using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using Model.DTO;

namespace Business.Interfaces
{
    public interface ICustomerBusinessRules
    {
        Task ValidateCustomerAsync(CreateCustomerDto customer);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Customer>> SearchCustomersAsync(string term);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> CanDeleteCustomerAsync(string username);
        Task<bool> DeleteCustomerAsync(string username);
        Task<bool> UpdateNotesAsync(string username, string notes);
    }
}
