using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Model.DomainModels;
using Optional.Caching;

namespace DataAccess.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<List<Supplier>> GetSupplierByTermAsync(string searchTerm);
        Task<bool> AddSupplierAsync(Supplier supplier);
        Task<bool> UpdateCustomerAsync(Supplier supplier);    
        Task<bool> DeleteSupplierAsync(int id);
        Task<bool> SupplierExistsAsync(int id);
    }
    
}
