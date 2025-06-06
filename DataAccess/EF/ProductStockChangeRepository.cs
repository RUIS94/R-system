﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.DomainModels;

namespace DataAccess.EF
{
    public class ProductStockChangeRepository : IProductStockChangeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductStockChangeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductStockChange>> GetAllAsync()
        {
            return await _context.ProductStockChanges.Include(p => p.Product).ToListAsync();
        }

        public async Task<ProductStockChange?> GetByIdAsync(int id)
        {
            return await _context.ProductStockChanges
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task AddAsync(ProductStockChange change)
        {
            _context.ProductStockChanges.Add(change);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(ProductStockChange change)
        {
            _context.ProductStockChanges.Update(change);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductStockChanges.FindAsync(id);
            if (entity == null) return false;
            _context.ProductStockChanges.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}