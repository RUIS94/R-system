using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<List<CustomerOrder>> GetAllOrdersAsync();
        Task<List<CustomerOrder>> GetOrdersByCustomerIdAsync(int customerId);
        Task<bool> CreateOrderAsync(CustomerOrder order);
        Task<bool> UpdateOrderAsync(CustomerOrder order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
