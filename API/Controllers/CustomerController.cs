using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerWithAccount([FromBody] CreateCustomerDto dto)
        {
            try
            {
                await _customerService.CreateCustomerWithAccountAsync(dto);
                return Ok(new { message = "The customer and account have been successfully created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
