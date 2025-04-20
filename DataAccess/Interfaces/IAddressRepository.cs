using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId);
        Task<bool> AddAddressAsync(Address address);
        Task<bool> UpdateAddressesByCustomerIdAsync(int customerId, List<Address> addresses);
        Task<bool> DeleteAddressesByCustomerIdAsync(int customerId);
    }
}
