using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.SupplierOrderDetails
{
    public class SupplierOrderDetailBusiness : ISupplierOrderDetailBusiness
    {
        private readonly ISupplierOrderDetailRepository _supplierOrderDetailRepository;

        public SupplierOrderDetailBusiness(ISupplierOrderDetailRepository supplierOrderDetailRepository)
        {
            _supplierOrderDetailRepository = supplierOrderDetailRepository;
        }

        public async Task<List<SupplierOrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _supplierOrderDetailRepository.GetAllOrderDetailsAsync();
        }

        public async Task<List<SupplierOrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId)
        {
            return await _supplierOrderDetailRepository.GetOrderDetailsByOrderIDAsync(orderId);
        }

        public async Task<bool> AddOrderDetailAsync(SupplierOrderDetail detail)
        {
            return await _supplierOrderDetailRepository.AddOrderDetailAsync(detail);
        }

        public async Task<bool> UpdateOrderDetailAsync(SupplierOrderDetail detail)
        {
            return await _supplierOrderDetailRepository.UpdateOrderDetailAsync(detail);
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            return await _supplierOrderDetailRepository.DeleteOrderDetailAsync(id);
        }
    }
}
