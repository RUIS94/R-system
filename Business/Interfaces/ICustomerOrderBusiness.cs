using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface ICustomerOrderBusiness
    {
        Task<List<CustomerOrder>> GetAllOrdersAsync();
        Task<List<CustomerOrder>> GetOrdersByCustomerIdAsync(int customerId);
        Task<bool> AddOrderAsync(CustomerOrder order);
        Task<bool> UpdateOrderAsync(CustomerOrder order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
