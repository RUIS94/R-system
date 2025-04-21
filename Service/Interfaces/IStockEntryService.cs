using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IStockEntryService
    {
        Task<List<StockEntry>> GetAllStockEntriesAsync();
        Task<List<StockEntry>> GetStockEntriesBySupplierOrderIdAsync(Guid supplierOrderId);
        Task<bool> AddStockEntryAsync(StockEntry entry);
        Task<bool> UpdateStockEntryAsync(StockEntry entry);
        Task<bool> DeleteStockEntryAsync(int id);
    }
}
