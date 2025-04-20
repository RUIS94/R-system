using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var products = await _productService.GetProductsByCategoryAsync(category);
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var products = await _productService.SearchProductsByNameAsync(name);
            return Ok(products);
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var product = await _productService.GetProductByBarcodeAsync(barcode);
            if (product == null)
                return NotFound($"Product with barcode '{barcode}' not found");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            try
            {
                var success = await _productService.AddProductAsync(product);
                return success ? Ok("Product added successfully") : BadRequest("Failed to add product");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            try
            {
                var success = await _productService.UpdateProductAsync(product);
                return success ? Ok("Product updated successfully") : NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _productService.DeleteProductByIdAsync(id);
                return success ? Ok("Product deleted successfully") : NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/inventory")]
        public async Task<IActionResult> UpdateInventory(int id, [FromQuery] int inventory)
        {
            try
            {
                var success = await _productService.UpdateInventoryAsync(id, inventory);
                return success ? Ok("Inventory updated successfully") : BadRequest("Failed to update inventory");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}