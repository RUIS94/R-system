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
    public class ProductSupplierService : IProductSupplierService
    {
        private readonly IProductSupplierBusiness _business;
        private readonly TransactionExecutor _executor;

        public ProductSupplierService(IProductSupplierBusiness business, IUnitOfWork unitOfWork)
        {
            _business = business;
            _executor = new TransactionExecutor(unitOfWork);
        }

        public async Task<List<ProductSupplier>> GetAllAsync() => await _business.GetAllAsync();
        public async Task<ProductSupplier?> GetByIdAsync(int id) => await _business.GetByIdAsync(id);

        public async Task<bool> AddAsync(ProductSupplier productSupplier)
        {
            return await _executor.ExecuteAsync(async () =>
            {
                await _business.AddAsync(productSupplier);
                return true;
            });
        }

        public async Task<bool> UpdateAsync(ProductSupplier productSupplier)
        {
            return await _executor.ExecuteAsync(() => _business.UpdateAsync(productSupplier));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _executor.ExecuteAsync(() => _business.DeleteAsync(id));
        }
    }
}
