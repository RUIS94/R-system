using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IProductSupplierService
    {
        Task<List<ProductSupplier>> GetAllAsync();
        Task<ProductSupplier?> GetByIdAsync(int id);
        Task<bool> AddAsync(ProductSupplier productSupplier);
        Task<bool> UpdateAsync(ProductSupplier productSupplier);
        Task<bool> DeleteAsync(int id);
    }
}
