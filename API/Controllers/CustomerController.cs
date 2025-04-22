using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
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
        [HttpGet("allcustomer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(new { data = customers });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string term)
        {
            var result = await _customerService.SearchCustomersAsync(term);
            return Ok(new { data = result });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomerWithAccount([FromBody] CreateCustomerDto dto)
        {
            await _customerService.CreateCustomerWithAccountAsync(dto);
            return Ok(new { message = "The customer and account have been successfully created" });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto dto)
        {
            var success = await _customerService.UpdateCustomerAsync(dto);
            if (!success)
                return NotFound(new { message = "Customer not found" });

            return Ok(new { message = "Customer updated successfully" });
        }

        [HttpPost("update-notes")]
        public async Task<IActionResult> UpdateNotes([FromBody] UpdateCustomerNotesDto dto)
        {
            var success = await _customerService.UpdateNotesAsync(dto);
            if (!success)
                return NotFound(new { message = "Customer not found or update failed" });

            return Ok(new { message = "Notes updated successfully" });
        }

        [HttpDelete("delete/{username}")]
        public async Task<IActionResult> DeleteCustomerWithAccount(string username)
        {
            var success = await _customerService.DeleteCustomerWithAccountAsync(username);
            if (!success)
                return NotFound(new { message = "Customer not found or could not be deleted" });

            return Ok(new { message = "Customer and account deleted successfully" });
        }

    }
}
