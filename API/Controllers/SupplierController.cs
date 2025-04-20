using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetSuppliersAsync()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Supplier>>> SearchSuppliersAsync([FromQuery] string term)
        {
            var suppliers = await _supplierService.SearchSuppliersAsync(term);
            return Ok(suppliers);
        }

        [HttpPost]
        public async Task<ActionResult> AddSupplierAsync([FromBody] Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest("Supplier data is required.");
            }

            bool isSuccess = await _supplierService.AddSupplierAsync(supplier);
            if (isSuccess)
            {
                return CreatedAtAction(nameof(GetSuppliersAsync), new { id = supplier.ID }, supplier);
            }
            return BadRequest("Failed to add supplier.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSupplierAsync(int id, [FromBody] Supplier supplier)
        {
            if (id != supplier.ID)
            {
                return BadRequest("Supplier ID mismatch.");
            }

            bool isSuccess = await _supplierService.UpdateSupplierAsync(supplier);
            if (isSuccess)
            {
                return NoContent(); // HTTP 204, No Content
            }
            return NotFound($"Supplier with ID {id} not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSupplierAsync(int id)
        {
            bool isSuccess = await _supplierService.DeleteSupplierAsync(id);
            if (isSuccess)
            {
                return NoContent(); // HTTP 204, No Content
            }
            return NotFound($"Supplier with ID {id} not found.");
        }
    }
}
