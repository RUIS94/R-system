using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface ISupplierOrderDetailService
    {
        Task<List<SupplierOrderDetail>> GetAllOrderDetailsAsync();
        Task<List<SupplierOrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId);
        Task<bool> AddOrderDetailAsync(SupplierOrderDetail detail);
        Task<bool> UpdateOrderDetailAsync(SupplierOrderDetail detail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }

}
