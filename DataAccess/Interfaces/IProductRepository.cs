using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using StackExchange.Redis;

namespace DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(string category);
        Task<Product?> GetProductByBarcodeAsync(string barcode);
        Task<bool> DeleteProductByIdAsync(int id);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> UpdateInventoryAsync(int id, int inventory);
        Task<List<Product>> GetProductBynameAsync(string name);
        Task<bool> ProductExistsAsync(int id);
    }
}
