using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IProductStockChangeRepository
    {
        Task<List<ProductStockChange>> GetAllAsync();
        Task<ProductStockChange?> GetByIdAsync(int id);
        Task AddAsync(ProductStockChange change);
        Task<bool> UpdateAsync(ProductStockChange change);
        Task<bool> DeleteAsync(int id);
    }
}
