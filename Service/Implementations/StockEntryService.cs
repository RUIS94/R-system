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
    public class StockEntryService : IStockEntryService
    {
        private readonly IStockEntryBusiness _stockEntryBusiness;

        public StockEntryService(IStockEntryBusiness stockEntryBusiness)
        {
            _stockEntryBusiness = stockEntryBusiness;
        }

        public async Task<List<StockEntry>> GetAllStockEntriesAsync()
        {
            return await _stockEntryBusiness.GetAllStockEntriesAsync();
        }

        public async Task<List<StockEntry>> GetStockEntriesBySupplierOrderIdAsync(Guid supplierOrderId)
        {
            return await _stockEntryBusiness.GetStockEntriesBySupplierOrderIdAsync(supplierOrderId);
        }

        public async Task<bool> AddStockEntryAsync(StockEntry entry)
        {
            return await _stockEntryBusiness.AddStockEntryAsync(entry);
        }

        public async Task<bool> UpdateStockEntryAsync(StockEntry entry)
        {
            return await _stockEntryBusiness.UpdateStockEntryAsync(entry);
        }

        public async Task<bool> DeleteStockEntryAsync(int id)
        {
            return await _stockEntryBusiness.DeleteStockEntryAsync(id);
        }
    }
}
