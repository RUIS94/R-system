using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.DomainModels;

namespace DataAccess.EF
{
    public class ProductSupplierRepository : IProductSupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductSupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSupplier>> GetAllAsync()
        {
            return await _context.ProductSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<ProductSupplier?> GetByIdAsync(int id)
        {
            return await _context.ProductSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddAsync(ProductSupplier productSupplier)
        {
            _context.ProductSuppliers.Add(productSupplier);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(ProductSupplier productSupplier)
        {
            _context.ProductSuppliers.Update(productSupplier);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductSuppliers.FindAsync(id);
            if (entity == null) return false;
            _context.ProductSuppliers.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
