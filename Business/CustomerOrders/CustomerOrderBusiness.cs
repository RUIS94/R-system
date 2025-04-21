using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.CustomerOrders
{
    public class CustomerOrderBusiness : ICustomerOrderBusiness
    {
        private readonly ICustomerOrderRepository _orderRepository;

        public CustomerOrderBusiness(ICustomerOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<CustomerOrder>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllCustomerOrdersAsync();
        }

        public async Task<List<CustomerOrder>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _orderRepository.GetCustomerOrdersByCustomerIDAsync(customerId);
        }

        public async Task<bool> AddOrderAsync(CustomerOrder order)
        {
            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<bool> UpdateOrderAsync(CustomerOrder order)
        {
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
