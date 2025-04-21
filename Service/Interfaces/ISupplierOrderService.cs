using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface ISupplierOrderService
    {
        Task<List<SupplierOrder>> GetAllSupplierOrdersAsync();
        Task<List<SupplierOrder>> GetSupplierOrdersBySupplierIdAsync(int id);
        Task<bool> AddOrderAsync(SupplierOrder order);
        Task<bool> UpdateOrderAsync(SupplierOrder order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
