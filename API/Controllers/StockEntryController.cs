using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockEntryController : ControllerBase
    {
        private readonly IStockEntryService _stockEntryService;

        public StockEntryController(IStockEntryService stockEntryService)
        {
            _stockEntryService = stockEntryService;
        }

        // 获取所有库存入库记录
        [HttpGet]
        public async Task<ActionResult<List<StockEntry>>> GetAllStockEntries()
        {
            var entries = await _stockEntryService.GetAllStockEntriesAsync();
            return Ok(entries); // 返回200状态和库存入库记录列表
        }

        // 根据供应商订单ID获取入库记录
        [HttpGet("byOrder/{supplierOrderId}")]
        public async Task<ActionResult<List<StockEntry>>> GetStockEntriesBySupplierOrderId(Guid supplierOrderId)
        {
            var entries = await _stockEntryService.GetStockEntriesBySupplierOrderIdAsync(supplierOrderId);
            if (entries == null || entries.Count == 0)
            {
                return NotFound(); // 如果没有找到，返回404
            }
            return Ok(entries); // 返回200状态和找到的入库记录
        }

        // 创建新的库存入库记录
        [HttpPost]
        public async Task<ActionResult> CreateStockEntry([FromBody] StockEntry entry)
        {
            if (entry == null)
            {
                return BadRequest(); // 请求体为空，返回400错误
            }

            var result = await _stockEntryService.AddStockEntryAsync(entry);
            if (result)
            {
                return CreatedAtAction(nameof(GetStockEntriesBySupplierOrderId), new { supplierOrderId = entry.SupplierOrderID }, entry); // 返回201创建状态
            }
            return BadRequest(); // 如果创建失败，返回400错误
        }

        // 更新现有库存入库记录
        [HttpPut]
        public async Task<ActionResult> UpdateStockEntry([FromBody] StockEntry entry)
        {
            if (entry == null)
            {
                return BadRequest(); // 请求体为空，返回400错误
            }

            var result = await _stockEntryService.UpdateStockEntryAsync(entry);
            if (result)
            {
                return NoContent(); // 返回204无内容状态，表示成功但不返回内容
            }
            return NotFound(); // 如果未找到，返回404错误
        }

        // 删除库存入库记录
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStockEntry(int id)
        {
            var result = await _stockEntryService.DeleteStockEntryAsync(id);
            if (result)
            {
                return NoContent(); // 返回204无内容状态
            }
            return NotFound(); // 如果未找到，返回404错误
        }
    }
}