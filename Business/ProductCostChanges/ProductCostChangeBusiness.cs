using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.ProductCostChanges
{
    public class ProductCostChangeBusiness : IProductCostChangeBusiness
    {
        private readonly IProductCostChangeRepository _repository;

        public ProductCostChangeBusiness(IProductCostChangeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductCostChange>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<ProductCostChange?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(ProductCostChange change)
        {
            await _repository.AddAsync(change);
        }

        public async Task UpdateAsync(ProductCostChange change)
        {
            await _repository.UpdateAsync(change);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
