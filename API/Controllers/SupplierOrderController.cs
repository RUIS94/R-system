using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrderService _supplierOrderService;

        public SupplierOrderController(ISupplierOrderService supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        // 获取所有供应商订单
        [HttpGet]
        public async Task<ActionResult<List<SupplierOrder>>> GetAllSupplierOrders()
        {
            var orders = await _supplierOrderService.GetAllSupplierOrdersAsync();
            return Ok(orders); // 返回200状态和订单列表
        }

        // 根据供应商ID获取订单
        [HttpGet("{supplierId}")]
        public async Task<ActionResult<List<SupplierOrder>>> GetSupplierOrdersBySupplierId(int supplierId)
        {
            var orders = await _supplierOrderService.GetSupplierOrdersBySupplierIdAsync(supplierId);
            if (orders == null || orders.Count == 0)
            {
                return NotFound(); // 如果没有找到，返回404
            }
            return Ok(orders); // 返回200状态和找到的订单
        }

        // 创建新的供应商订单
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] SupplierOrder order)
        {
            if (order == null)
            {
                return BadRequest(); // 请求体为空，返回400错误
            }

            var result = await _supplierOrderService.AddOrderAsync(order);
            if (result)
            {
                return CreatedAtAction(nameof(GetSupplierOrdersBySupplierId), new { supplierId = order.SupplierID }, order); // 返回201创建状态
            }
            return BadRequest(); // 如果创建失败，返回400错误
        }

        // 更新现有供应商订单
        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] SupplierOrder order)
        {
            if (order == null)
            {
                return BadRequest(); // 请求体为空，返回400错误
            }

            var result = await _supplierOrderService.UpdateOrderAsync(order);
            if (result)
            {
                return NoContent(); // 返回204无内容状态，表示成功但不返回内容
            }
            return NotFound(); // 如果未找到，返回404错误
        }

        // 删除供应商订单
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _supplierOrderService.DeleteOrderAsync(id);
            if (result)
            {
                return NoContent(); // 返回204无内容状态
            }
            return NotFound(); // 如果未找到，返回404错误
        }
    }
}
