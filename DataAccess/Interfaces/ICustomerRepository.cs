using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Customer>> GetCustomerByTermAsync(string searchTerm);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(string username);
        Task<bool> UpdateNotesAsync(string username, string notes);
        Task<int> GetIDBynameAsync(string username);
    }
}
