using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using StackExchange.Redis;

namespace DataAccess.Interfaces
{
    public interface ICustomerOrderRepository
    {
        Task<List<CustomerOrder>> GetAllCustomerOrdersAsync();
        Task<List<CustomerOrder>> GetCustomerOrdersByCustomerIDAsync(int id);
        Task<bool> AddOrderAsync(CustomerOrder order);
        Task<bool> UpdateOrderAsync(CustomerOrder order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
