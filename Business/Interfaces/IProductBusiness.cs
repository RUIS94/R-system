using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface IProductBusiness
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(string category);
        Task<List<Product>> SearchProductsByNameAsync(string name);
        Task<Product?> GetProductByBarcodeAsync(string barcode);
        Task<bool> DeleteProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateInventoryAsync(int id, int inventory);
        Task<bool> ProductExistsAsync(int id);
    }
}
