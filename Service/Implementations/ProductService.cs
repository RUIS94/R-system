using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Products;
using DataAccess.Interfaces;
using Model.DomainModels;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionExecutor _transactionExecutor;

        public ProductService(IProductBusiness productBusiness, IUnitOfWork unitOfWork)
        {
            _productBusiness = productBusiness;
            _unitOfWork = unitOfWork;
            _transactionExecutor = new TransactionExecutor(_unitOfWork);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productBusiness.GetAllProductsAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _productBusiness.GetProductsByCategoryAsync(category);
        }

        public async Task<List<Product>> SearchProductsByNameAsync(string name)
        {
            return await _productBusiness.SearchProductsByNameAsync(name);
        }

        public async Task<Product?> GetProductByBarcodeAsync(string barcode)
        {
            return await _productBusiness.GetProductByBarcodeAsync(barcode);
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _productBusiness.DeleteProductByIdAsync(id),
                "Delete failed: Product not found.");
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _productBusiness.UpdateProductAsync(product),
                "Update failed: Product does not exist.");
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _productBusiness.AddProductAsync(product),
                "Add failed: Product already exists.");
        }

        public async Task<bool> UpdateInventoryAsync(int id, int inventory)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _productBusiness.UpdateInventoryAsync(id, inventory),
                "Inventory update failed.");
        }
    }
}