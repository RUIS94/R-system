using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IProductPriceChangeService
    {
        Task<List<ProductPriceChange>> GetAllAsync();
        Task<ProductPriceChange?> GetByIdAsync(int id);

        Task<bool> AddAsync(ProductPriceChange change);

        Task<bool> UpdateAsync(ProductPriceChange change);

        Task<bool> DeleteAsync(int id);
    }
}
