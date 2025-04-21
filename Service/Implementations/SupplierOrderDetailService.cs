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
    public class SupplierOrderDetailService : ISupplierOrderDetailService
    {
        private readonly ISupplierOrderDetailBusiness _supplierOrderDetailBusiness;

        public SupplierOrderDetailService(ISupplierOrderDetailBusiness supplierOrderDetailBusiness)
        {
            _supplierOrderDetailBusiness = supplierOrderDetailBusiness;
        }

        public async Task<List<SupplierOrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _supplierOrderDetailBusiness.GetAllOrderDetailsAsync();
        }

        public async Task<List<SupplierOrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId)
        {
            return await _supplierOrderDetailBusiness.GetOrderDetailsByOrderIdAsync(orderId);
        }

        public async Task<bool> AddOrderDetailAsync(SupplierOrderDetail detail)
        {
            return await _supplierOrderDetailBusiness.AddOrderDetailAsync(detail);
        }

        public async Task<bool> UpdateOrderDetailAsync(SupplierOrderDetail detail)
        {
            return await _supplierOrderDetailBusiness.UpdateOrderDetailAsync(detail);
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await _supplierOrderDetailBusiness.DeleteOrderDetailAsync(id);
        }
    }
}
