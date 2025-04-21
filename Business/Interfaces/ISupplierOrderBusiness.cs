using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface ISupplierOrderBusiness
    {
        Task<List<SupplierOrder>> GetAllSupplierOrdersAsync();
        Task<List<SupplierOrder>> GetSupplierOrdersBySupplierIdAsync(int id);
        Task<bool> AddOrderAsync(SupplierOrder order);
        Task<bool> UpdateOrderAsync(SupplierOrder order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
