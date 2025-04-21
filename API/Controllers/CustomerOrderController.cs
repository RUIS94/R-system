using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Implementations;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        // 获取所有订单
        [HttpGet]
        public async Task<ActionResult<List<CustomerOrder>>> GetAllOrders()
        {
            var orders = await _customerOrderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // 根据客户ID获取订单
        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<CustomerOrder>>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _customerOrderService.GetOrdersByCustomerIdAsync(customerId);
            if (orders == null || orders.Count == 0)
            {
                return NotFound(); // 如果没有找到，返回404
            }
            return Ok(orders); // 返回找到的订单
        }

        // 创建新订单
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CustomerOrder order)
        {
            if (order == null)
            {
                return BadRequest(); // 请求体为空，返回400
            }

            var result = await _customerOrderService.CreateOrderAsync(order);
            if (result)
            {
                return CreatedAtAction(nameof(GetOrdersByCustomerId), new { customerId = order.CustomerID }, order); // 返回201创建状态
            }
            return BadRequest(); // 如果创建失败，返回400
        }

        // 更新订单
        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] CustomerOrder order)
        {
            if (order == null)
            {
                return BadRequest(); // 请求体为空，返回400
            }

            var result = await _customerOrderService.UpdateOrderAsync(order);
            if (result)
            {
                return NoContent(); // 返回204无内容状态
            }
            return NotFound(); // 如果未找到，返回404
        }

        // 删除订单
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _customerOrderService.DeleteOrderAsync(id);
            if (result)
            {
                return NoContent(); // 返回204无内容状态
            }
            return NotFound(); // 如果未找到，返回404
        }
    }
}

