﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface ICustomerOrderDetailBusiness
    {
        Task<List<CustomerOrderDetail>> GetAllOrderDetailsAsync();
        Task<List<CustomerOrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId);
        Task<bool> AddOrderDetailAsync(CustomerOrderDetail detail);
        Task<bool> UpdateOrderDetailAsync(CustomerOrderDetail detail);
        Task<bool> DeleteOrderDetailAsync(int id);
    }
}
