using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using Model.DTO;

namespace Business.Interfaces
{
    public interface IAddressBusiness
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<List<Address>> GetAddressesByCustomerAsync(string username);
        Task<bool> AddAddressAsync(string username, CreateAddressDto dto);
        Task<bool> UpdateAddressesByCustomernameAsync(string name, UpdateAddressDto dto);
        Task<bool> DeleteAddressesByCustomerAsync(string username);
    }
}
