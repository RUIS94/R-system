using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.Suppliers
{
    public class SupplierBusiness : ISupplierBusiness
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierBusiness(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllSuppliersAsync();
        }

        public async Task<List<Supplier>> SearchSuppliersAsync(string term)
        {
            return await _supplierRepository.GetSupplierByTermAsync(term);
        }

        public async Task<bool> AddSupplierAsync(Supplier supplier)
        {
            SupplierValidator.Validate(supplier);
            return await _supplierRepository.AddSupplierAsync(supplier);
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            if (!await _supplierRepository.SupplierExistsAsync(supplier.ID))
                throw new KeyNotFoundException($"Supplier with ID {supplier.ID} does not exist");
            return await _supplierRepository.UpdateCustomerAsync(supplier);
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            if (!await _supplierRepository.SupplierExistsAsync(id))
                throw new KeyNotFoundException($"Supplier with ID {id} does not exist");
            return await _supplierRepository.DeleteSupplierAsync(id);
        }
    }
}
