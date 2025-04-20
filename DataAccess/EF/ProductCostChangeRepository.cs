using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.DomainModels;

namespace DataAccess.EF
{
    public class ProductCostChangeRepository : IProductCostChangeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCostChangeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCostChange>> GetAllAsync()
        {
            return await _context.ProductCostChanges
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<ProductCostChange?> GetByIdAsync(int id)
        {
            return await _context.ProductCostChanges
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddAsync(ProductCostChange change)
        {
            _context.ProductCostChanges.Add(change);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCostChange change)
        {
            _context.ProductCostChanges.Update(change);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.ProductCostChanges.FindAsync(id);
            if (existing != null)
            {
                _context.ProductCostChanges.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
