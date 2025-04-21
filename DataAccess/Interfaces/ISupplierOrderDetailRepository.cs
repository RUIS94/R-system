using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface ISupplierOrderDetailRepository
    {
        Task<List<SupplierOrderDetail>> GetAllOrderDetailsAsync();
        Task<List<SupplierOrderDetail>> GetOrderDetailsByOrderIDAsync(Guid orderId);
        Task<bool> AddOrderDetailAsync(SupplierOrderDetail detail);
        Task<bool> UpdateOrderDetailAsync(SupplierOrderDetail detail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
