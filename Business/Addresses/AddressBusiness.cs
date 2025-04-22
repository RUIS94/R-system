using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;

namespace Business.Addresses
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;

        public AddressBusiness(IAddressRepository addressRepository, ICustomerRepository customerRepository)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _addressRepository.GetAllAddressesAsync();
        }

        public async Task<List<Address>> GetAddressesByCustomerAsync(string username)
        {
            int customerId = await _customerRepository.GetIDBynameAsync(username);
            return await _addressRepository.GetAddressesByCustomerIdAsync(customerId);
        }

        public async Task<bool> AddAddressAsync(string username, CreateAddressDto dto)
        {
            int customerId = await _customerRepository.GetIDBynameAsync(username);
            var address = new Address
            {
                CustomerID = customerId,
                StreetAddress = dto.StreetAddress,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                Country = dto.Country
            };
            return await _addressRepository.AddAddressAsync(address);
        }

        public async Task<bool> UpdateAddressesByCustomernameAsync(string username, UpdateAddressDto dto)
        {
            int customerId = await _customerRepository.GetIDBynameAsync(username);
            var address = new Address
            {
                CustomerID = customerId,
                StreetAddress = dto.StreetAddress,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                Country = dto.Country
            };
            return await _addressRepository.UpdateAddressesByCustomerIdAsync(address);
        }

        public async Task<bool> DeleteAddressesByCustomerAsync(string username)
        {
            int customerId = await _customerRepository.GetIDBynameAsync(username);
            return await _addressRepository.DeleteAddressesByCustomerIdAsync(customerId);
        }
    }
}
