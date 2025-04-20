using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.ProductPriceChanges
{
    public class ProductPriceChangeBusiness : IProductPriceChangeBusiness
    {
        private readonly IProductPriceChangeRepository _repository;

        public ProductPriceChangeBusiness(IProductPriceChangeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductPriceChange>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<ProductPriceChange?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(ProductPriceChange change) => await _repository.AddAsync(change);
        public async Task<bool> UpdateAsync(ProductPriceChange change) => await _repository.UpdateAsync(change);
        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
