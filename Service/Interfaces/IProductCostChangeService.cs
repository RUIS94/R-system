using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IProductCostChangeService
    {
        Task<List<ProductCostChange>> GetAllAsync();
        Task<ProductCostChange?> GetByIdAsync(int id);
        Task AddAsync(ProductCostChange change);
        Task UpdateAsync(ProductCostChange change);
        Task DeleteAsync(int id);
    }
}
