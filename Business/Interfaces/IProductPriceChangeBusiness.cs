using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface IProductPriceChangeBusiness
    {
        Task<List<ProductPriceChange>> GetAllAsync();
        Task<ProductPriceChange?> GetByIdAsync(int id);
        Task AddAsync(ProductPriceChange change);
        Task<bool> UpdateAsync(ProductPriceChange change);
        Task<bool> DeleteAsync(int id);
    }
}
