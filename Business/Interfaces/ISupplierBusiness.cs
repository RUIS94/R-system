using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Suppliers;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface ISupplierBusiness
    {
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<List<Supplier>> SearchSuppliersAsync(string term);
        Task<bool> AddSupplierAsync(Supplier supplier);
        Task<bool> UpdateSupplierAsync(Supplier supplier);
        Task<bool> DeleteSupplierAsync(int id);
    }
}
