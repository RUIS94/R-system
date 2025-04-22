using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using Model.DTO;

namespace Business.Interfaces
{
    public interface ICustomerBusiness
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Customer>> SearchCustomersAsync(string term);
        Task<bool> UpdateCustomerAsync(UpdateCustomerDto dto);
        Task<bool> UpdateNotesAsync(UpdateCustomerNotesDto dto);
        Task<bool> CreateCustomerWithAccountAsync(CreateCustomerDto dto);
        Task<bool> DeleteCustomerWithAccountAsync(string username);
    }
}
