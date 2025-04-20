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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierBusiness _supplierBusiness;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionExecutor _transactionExecutor;

        public SupplierService(ISupplierBusiness supplierBusiness, IUnitOfWork unitOfWork)
        {
            _supplierBusiness = supplierBusiness;
            _unitOfWork = unitOfWork;
            _transactionExecutor = new TransactionExecutor(_unitOfWork);
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierBusiness.GetAllSuppliersAsync();
        }

        public async Task<List<Supplier>> SearchSuppliersAsync(string term)
        {
            return await _supplierBusiness.SearchSuppliersAsync(term);
        }

        public async Task<bool> AddSupplierAsync(Supplier supplier)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _supplierBusiness.AddSupplierAsync(supplier));
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _supplierBusiness.UpdateSupplierAsync(supplier));
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _supplierBusiness.DeleteSupplierAsync(id),
                "Delete failed");
        }
    }
}
