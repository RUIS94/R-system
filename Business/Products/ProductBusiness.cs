using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Validation;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.Products
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _productRepository.GetProductsByCategoryAsync(category);
        }

        public async Task<List<Product>> SearchProductsByNameAsync(string name)
        {
            return await _productRepository.GetProductBynameAsync(name);
        }

        public async Task<Product?> GetProductByBarcodeAsync(string barcode)
        {
            return await _productRepository.GetProductByBarcodeAsync(barcode);
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            return await _productRepository.DeleteProductByIdAsync(id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var exists = await _productRepository.GetProductByBarcodeAsync(product.Barcode);
            if (exists == null)
            {
                throw new KeyNotFoundException($"Product with Bartcode '{product.Barcode}' does not exist.");
            }
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            ProductValidator.Validate(product);
            var existing = await _productRepository.GetProductByBarcodeAsync(product.Barcode!);
            if (existing != null)
                throw new Exception("A product with the same barcode already exists.");
            if (product.Price < product.Cost)
                throw new Exception("Price should not be less than cost.");
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> UpdateInventoryAsync(int id, int inventory)
        {
            return await _productRepository.UpdateInventoryAsync(id, inventory);
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _productRepository.ProductExistsAsync(id);
        }
    }
}
