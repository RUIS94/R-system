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
    public class ProductPriceChangeRepository : IProductPriceChangeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductPriceChangeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductPriceChange>> GetAllAsync()
        {
            return await _context.ProductPriceChanges
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<ProductPriceChange?> GetByIdAsync(int id)
        {
            return await _context.ProductPriceChanges
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddAsync(ProductPriceChange change)
        {
            _context.ProductPriceChanges.Add(change);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(ProductPriceChange change)
        {
            _context.ProductPriceChanges.Update(change);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductPriceChanges.FindAsync(id);
            if (entity == null) return false;
            _context.ProductPriceChanges.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
