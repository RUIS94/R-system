using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using Model.DTO;

namespace Service.Interfaces
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<List<Address>> GetAddressesByCustomerAsync(string username);
        Task<bool> AddAddressAsync(string username, CreateAddressDto dto);
        Task<bool> UpdateAddressesByCustomernameAsync(string username, UpdateAddressDto dto);
        Task<bool> DeleteAddressesByCustomerAsync(string username);
    }
}
