using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.Addresses
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IAddressRepository _addressRepository;

        public AddressBusiness(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _addressRepository.GetAllAddressesAsync();
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId)
        {
            return await _addressRepository.GetAddressesByCustomerIdAsync(customerId);
        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            // 可以添加 AddressValidator.Validate(address); 如需验证
            return await _addressRepository.AddAddressAsync(address);
        }

        public async Task<bool> UpdateAddressesByCustomerIdAsync(int customerId, List<Address> addresses)
        {
            return await _addressRepository.UpdateAddressesByCustomerIdAsync(customerId, addresses);
        }

        public async Task<bool> DeleteAddressesByCustomerIdAsync(int customerId)
        {
            return await _addressRepository.DeleteAddressesByCustomerIdAsync(customerId);
        }
    }
}
