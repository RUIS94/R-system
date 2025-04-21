using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.SupplierOrders
{
    public class SupplierOrderBusiness : ISupplierOrderBusiness
    {
        private readonly ISupplierOrderRepository _supplierOrderRepository;

        public SupplierOrderBusiness(ISupplierOrderRepository supplierOrderRepository)
        {
            _supplierOrderRepository = supplierOrderRepository;
        }

        public async Task<List<SupplierOrder>> GetAllSupplierOrdersAsync()
        {
            return await _supplierOrderRepository.GetAllSupplierOrdersAsync();
        }

        public async Task<List<SupplierOrder>> GetSupplierOrdersBySupplierIdAsync(int id)
        {
            return await _supplierOrderRepository.GetSupplierOrdersBySupplierIDAsync(id);
        }

        public async Task<bool> AddOrderAsync(SupplierOrder order)
        {
            return await _supplierOrderRepository.AddOrderAsync(order);
        }

        public async Task<bool> UpdateOrderAsync(SupplierOrder order)
        {
            return await _supplierOrderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _supplierOrderRepository.DeleteOrderAsync(id);
        }
    }
}
