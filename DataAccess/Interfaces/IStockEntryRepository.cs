using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IStockEntryRepository
    {
        Task<List<StockEntry>> GetAllStockEntriesAsync(); 
        Task<List<StockEntry>> GetStockEntriesBySupplierOrderIDAsync(Guid supplierOrderId); 
        Task<bool> AddStockEntryAsync(StockEntry entry); 
        Task<bool> UpdateStockEntryAsync(StockEntry entry); 
        Task<bool> DeleteStockEntryAsync(int id); 
    }
}
