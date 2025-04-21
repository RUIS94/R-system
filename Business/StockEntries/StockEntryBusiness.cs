using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.StockEntries
{
    public class StockEntryBusiness : IStockEntryBusiness
    {
        private readonly IStockEntryRepository _stockEntryRepository;

        public StockEntryBusiness(IStockEntryRepository stockEntryRepository)
        {
            _stockEntryRepository = stockEntryRepository;
        }

        public async Task<List<StockEntry>> GetAllStockEntriesAsync()
        {
            return await _stockEntryRepository.GetAllStockEntriesAsync();
        }

        public async Task<List<StockEntry>> GetStockEntriesBySupplierOrderIdAsync(Guid supplierOrderId)
        {
            return await _stockEntryRepository.GetStockEntriesBySupplierOrderIDAsync(supplierOrderId);
        }

        public async Task<bool> AddStockEntryAsync(StockEntry entry)
        {
            return await _stockEntryRepository.AddStockEntryAsync(entry);
        }

        public async Task<bool> UpdateStockEntryAsync(StockEntry entry)
        {
            return await _stockEntryRepository.UpdateStockEntryAsync(entry);
        }

        public async Task<bool> DeleteStockEntryAsync(int id)
        {
            return await _stockEntryRepository.DeleteStockEntryAsync(id);
        }
    }
}
