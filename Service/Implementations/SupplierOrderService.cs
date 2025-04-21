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
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly ISupplierOrderBusiness _supplierOrderBusiness;

        public SupplierOrderService(ISupplierOrderBusiness supplierOrderBusiness)
        {
            _supplierOrderBusiness = supplierOrderBusiness;
        }

        public async Task<List<SupplierOrder>> GetAllSupplierOrdersAsync()
        {
            return await _supplierOrderBusiness.GetAllSupplierOrdersAsync();
        }

        public async Task<List<SupplierOrder>> GetSupplierOrdersBySupplierIdAsync(int id)
        {
            return await _supplierOrderBusiness.GetSupplierOrdersBySupplierIdAsync(id);
        }

        public async Task<bool> AddOrderAsync(SupplierOrder order)
        {
            return await _supplierOrderBusiness.AddOrderAsync(order);
        }

        public async Task<bool> UpdateOrderAsync(SupplierOrder order)
        {
            return await _supplierOrderBusiness.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _supplierOrderBusiness.DeleteOrderAsync(id);
        }
    }
}
