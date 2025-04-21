using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.CustomerOrderDetails
{
    public class CustomerOrderDetailBusiness : ICustomerOrderDetailBusiness
    {
        private readonly ICustomerOrderDetailRepository _orderDetailRepository;

        public CustomerOrderDetailBusiness(ICustomerOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<List<CustomerOrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _orderDetailRepository.GetAllOrderDetailsAsync();
        }

        public async Task<List<CustomerOrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId)
        {
            return await _orderDetailRepository.GetOrderDetailsByOrderIDAsync(orderId);
        }

        public async Task<bool> AddOrderDetailAsync(CustomerOrderDetail detail)
        {
            return await _orderDetailRepository.AddOrderDetailAsync(detail);
        }

        public async Task<bool> UpdateOrderDetailAsync(CustomerOrderDetail detail)
        {
            return await _orderDetailRepository.UpdateOrderDetailAsync(detail);
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await _orderDetailRepository.DeleteOrderDetailAsync(id);
        }
    }
}
