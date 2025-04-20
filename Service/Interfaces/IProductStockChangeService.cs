using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IProductStockChangeService
    {
        Task<List<ProductStockChange>> GetAllAsync();
        Task<ProductStockChange?> GetByIdAsync(int id);
        Task<bool> AddAsync(ProductStockChange change);
        Task<bool> UpdateAsync(ProductStockChange change);
        Task<bool> DeleteAsync(int id);
    }
}
