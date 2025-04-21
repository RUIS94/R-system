using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface ICustomerOrderDetailRepository
    {
        Task<List<CustomerOrderDetail>> GetAllOrderDetailsAsync();
        Task<List<CustomerOrderDetail>> GetOrderDetailsByOrderIDAsync(Guid orderId);
        Task<bool> AddOrderDetailAsync(CustomerOrderDetail detail);
        Task<bool> UpdateOrderDetailAsync(CustomerOrderDetail detail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
