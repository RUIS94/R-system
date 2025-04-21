using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.CustomerOrders;
using Business.Interfaces;
using Model.DomainModels;
using Service.Interfaces;

namespace Service.Implementations
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderBusiness _customerOrderBusiness;

        public CustomerOrderService(ICustomerOrderBusiness customerOrderBusiness)
        {
            _customerOrderBusiness = customerOrderBusiness;
        }

        public async Task<List<CustomerOrder>> GetAllOrdersAsync()
        {
            return await _customerOrderBusiness.GetAllOrdersAsync();
        }

        public async Task<List<CustomerOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _customerOrderBusiness.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<bool> CreateOrderAsync(CustomerOrder order)
        {
            return await _customerOrderBusiness.AddOrderAsync(order);
        }

        public async Task<bool> UpdateOrderAsync(CustomerOrder order)
        {
            return await _customerOrderBusiness.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _customerOrderBusiness.DeleteOrderAsync(id);
        }
    }
}
