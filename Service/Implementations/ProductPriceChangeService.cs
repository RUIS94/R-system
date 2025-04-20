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
    public class ProductPriceChangeService : IProductPriceChangeService
    {
        private readonly IProductPriceChangeBusiness _business;
        private readonly TransactionExecutor _executor;

        public ProductPriceChangeService(IProductPriceChangeBusiness business, IUnitOfWork unitOfWork)
        {
            _business = business;
            _executor = new TransactionExecutor(unitOfWork);
        }

        public async Task<List<ProductPriceChange>> GetAllAsync() => await _business.GetAllAsync();
        public async Task<ProductPriceChange?> GetByIdAsync(int id) => await _business.GetByIdAsync(id);

        public async Task<bool> AddAsync(ProductPriceChange change)
        {
            return await _executor.ExecuteAsync(async () =>
            {
                await _business.AddAsync(change);
                return true;
            });
        }

        public async Task<bool> UpdateAsync(ProductPriceChange change)
        {
            return await _executor.ExecuteAsync(() => _business.UpdateAsync(change));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _executor.ExecuteAsync(() => _business.DeleteAsync(id));
        }
    }
}
