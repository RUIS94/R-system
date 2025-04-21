using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IProductSupplierRepository
    {
        Task<List<ProductSupplier>> GetAllAsync();
        Task<ProductSupplier?> GetByIdAsync(int id);
        Task AddAsync(ProductSupplier productSupplier);
        Task<bool> UpdateAsync(ProductSupplier productSupplier);
        Task<bool> DeleteAsync(int id);
    }
}
