using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.ProductSuppliers
{
    public class ProductSupplierBusiness : IProductSupplierBusiness
    {
        private readonly IProductSupplierRepository _repository;

        public ProductSupplierBusiness(IProductSupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductSupplier>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<ProductSupplier?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(ProductSupplier productSupplier) => await _repository.AddAsync(productSupplier);
        public async Task<bool> UpdateAsync(ProductSupplier productSupplier) => await _repository.UpdateAsync(productSupplier);
        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
