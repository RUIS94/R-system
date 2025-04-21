using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Model.DomainModels;
using Service.Interfaces;

namespace Service.Implementations
{
    public class CustomerOrderDetailService : ICustomerOrderDetailService
    {
        private readonly ICustomerOrderDetailBusiness _customerOrderDetailBusiness;

        public CustomerOrderDetailService(ICustomerOrderDetailBusiness customerOrderDetailBusiness)
        {
            _customerOrderDetailBusiness = customerOrderDetailBusiness;
        }

        public async Task<List<CustomerOrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _customerOrderDetailBusiness.GetAllOrderDetailsAsync();
        }

        public async Task<List<CustomerOrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId)
        {
            return await _customerOrderDetailBusiness.GetOrderDetailsByOrderIdAsync(orderId);
        }

        public async Task<bool> AddOrderDetailAsync(CustomerOrderDetail detail)
        {
            return await _customerOrderDetailBusiness.AddOrderDetailAsync(detail);
        }

        public async Task<bool> UpdateOrderDetailAsync(CustomerOrderDetail detail)
        {
            return await _customerOrderDetailBusiness.UpdateOrderDetailAsync(detail);
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await _customerOrderDetailBusiness.DeleteOrderDetailAsync(id);
        }
    }
}
