using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IProductCostChangeRepository
    {
        Task<List<ProductCostChange>> GetAllAsync();
        Task<ProductCostChange?> GetByIdAsync(int id);
        Task AddAsync(ProductCostChange change);
        Task UpdateAsync(ProductCostChange change);
        Task DeleteAsync(int id);
    }
}
