using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCostChangeController : ControllerBase
    {
        private readonly IProductCostChangeService _service;

        public ProductCostChangeController(IProductCostChangeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCostChange change)
        {
            await _service.AddAsync(change);
            return Ok("Created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCostChange change)
        {
            if (id != change.ID)
                return BadRequest("ID mismatch");
            await _service.UpdateAsync(change);
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}

