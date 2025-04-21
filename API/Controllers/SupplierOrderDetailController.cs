using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderDetailController : ControllerBase
    {
        private readonly ISupplierOrderDetailService _supplierOrderDetailService;

        public SupplierOrderDetailController(ISupplierOrderDetailService supplierOrderDetailService)
        {
            _supplierOrderDetailService = supplierOrderDetailService;
        }

        // 获取所有订单详情
        [HttpGet]
        public async Task<ActionResult<List<SupplierOrderDetail>>> GetAllOrderDetails()
        {
            var details = await _supplierOrderDetailService.GetAllOrderDetailsAsync();
            return Ok(details); // 返回200状态和订单详情列表
        }

        // 根据订单ID获取订单详情
        [HttpGet("{orderId}")]
        public async Task<ActionResult<List<SupplierOrderDetail>>> GetOrderDetailsByOrderId(Guid orderId)
        {
            var details = await _supplierOrderDetailService.GetOrderDetailsByOrderIdAsync(orderId);
            if (details == null || details.Count == 0)
            {
                return NotFound(); // 如果没有找到，返回404
            }
            return Ok(details); // 返回200状态和找到的订单详情
        }

        // 创建新的订单详情
        [HttpPost]
        public async Task<ActionResult> CreateOrderDetail([FromBody] SupplierOrderDetail detail)
        {
            if (detail == null)
            {
                return BadRequest(); // 请求体为空，返回400错误
            }

            var result = await _supplierOrderDetailService.AddOrderDetailAsync(detail);
            if (result)
            {
                return CreatedAtAction(nameof(GetOrderDetailsByOrderId), new { orderId = detail.OrderID }, detail); // 返回201创建状态
            }
            return BadRequest(); // 如果创建失败，返回400错误
        }

        // 更新现有订单详情
        [HttpPut]
        public async Task<ActionResult> UpdateOrderDetail([FromBody] SupplierOrderDetail detail)
        {
            if (detail == null)
            {
                return BadRequest(); // 请求体为空，返回400错误
            }

            var result = await _supplierOrderDetailService.UpdateOrderDetailAsync(detail);
            if (result)
            {
                return NoContent(); // 返回204无内容状态，表示成功但不返回内容
            }
            return NotFound(); // 如果未找到，返回404错误
        }

        // 删除订单详情
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetail(int id)
        {
            var result = await _supplierOrderDetailService.DeleteOrderDetailAsync(id);
            if (result)
            {
                return NoContent(); // 返回204无内容状态
            }
            return NotFound(); // 如果未找到，返回404错误
        }
    }
}
