using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IAddressBusiness _addressBusiness;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionExecutor _transactionExecutor;

        public AddressService(IAddressBusiness addressBusiness, IUnitOfWork unitOfWork)
        {
            _addressBusiness = addressBusiness;
            _unitOfWork = unitOfWork;
            _transactionExecutor = new TransactionExecutor(_unitOfWork);
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _addressBusiness.GetAllAddressesAsync();
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId)
        {
            return await _addressBusiness.GetAddressesByCustomerIdAsync(customerId);
        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _addressBusiness.AddAddressAsync(address));
        }

        public async Task<bool> UpdateAddressesByCustomerIdAsync(int customerId, List<Address> addresses)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _addressBusiness.UpdateAddressesByCustomerIdAsync(customerId, addresses));
        }

        public async Task<bool> DeleteAddressesByCustomerIdAsync(int customerId)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _addressBusiness.DeleteAddressesByCustomerIdAsync(customerId));
        }
    }
}
