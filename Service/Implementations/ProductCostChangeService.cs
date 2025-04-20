using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Model.DomainModels;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ProductCostChangeService : IProductCostChangeService
    {
        private readonly IProductCostChangeBusiness _business;

        public ProductCostChangeService(IProductCostChangeBusiness business)
        {
            _business = business;
        }

        public async Task<List<ProductCostChange>> GetAllAsync() => await _business.GetAllAsync();

        public async Task<ProductCostChange?> GetByIdAsync(int id) => await _business.GetByIdAsync(id);

        public async Task AddAsync(ProductCostChange change) => await _business.AddAsync(change);

        public async Task UpdateAsync(ProductCostChange change) => await _business.UpdateAsync(change);

        public async Task DeleteAsync(int id) => await _business.DeleteAsync(id);
    }
}
