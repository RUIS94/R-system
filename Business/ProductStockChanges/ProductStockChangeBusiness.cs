using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.ProductStockChanges
{
    public class ProductStockChangeBusiness : IProductStockChangeBusiness
    {
        private readonly IProductStockChangeRepository _repository;

        public ProductStockChangeBusiness(IProductStockChangeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductStockChange>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<ProductStockChange?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(ProductStockChange change) => await _repository.AddAsync(change);
        public async Task<bool> UpdateAsync(ProductStockChange change) => await _repository.UpdateAsync(change);
        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
