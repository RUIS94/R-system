using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Model.DTO;
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

        public async Task<List<Address>> GetAddressesByCustomerAsync(string username)
        {
            return await _addressBusiness.GetAddressesByCustomerAsync(username);
        }

        public async Task<bool> AddAddressAsync(string username, CreateAddressDto dto)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _addressBusiness.AddAddressAsync(username, dto));
        }

        public async Task<bool> UpdateAddressesByCustomernameAsync(string username, UpdateAddressDto dto)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _addressBusiness.UpdateAddressesByCustomernameAsync(username, dto));
        }

        public async Task<bool> DeleteAddressesByCustomerAsync(string username)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _addressBusiness.DeleteAddressesByCustomerAsync(username));
        }
    }
}
