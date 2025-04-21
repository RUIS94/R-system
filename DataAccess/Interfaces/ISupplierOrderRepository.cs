using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface ISupplierOrderRepository
    {
        Task<List<SupplierOrder>> GetAllSupplierOrdersAsync();
        Task<List<SupplierOrder>> GetSupplierOrdersBySupplierIDAsync(int id);
        Task<bool> AddOrderAsync(SupplierOrder order);
        Task<bool> UpdateOrderAsync(SupplierOrder order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
