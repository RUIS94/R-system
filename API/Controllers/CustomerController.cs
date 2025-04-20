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
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(new { data = customers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomers([FromQuery] string term)
        {
            try
            {
                var result = await _customerService.SearchCustomersAsync(term);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                var success = await _customerService.UpdateCustomerAsync(customer);
                if (!success)
                    return NotFound(new { message = "Customer not found" });

                return Ok(new { message = "Customer updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("update-notes")]
        public async Task<IActionResult> UpdateNotes([FromQuery] string username, [FromBody] string notes)
        {
            try
            {
                var success = await _customerService.UpdateNotesAsync(username, notes);
                if (!success)
                    return NotFound(new { message = "Customer not found or update failed" });

                return Ok(new { message = "Notes updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete/{username}")]
        public async Task<IActionResult> DeleteCustomerWithAccount(string username)
        {
            try
            {
                var success = await _customerService.DeleteCustomerWithAccountAsync(username);
                if (!success)
                    return NotFound(new { message = "Customer not found or could not be deleted" });

                return Ok(new { message = "Customer and account deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
