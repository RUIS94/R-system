using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/address
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        // GET: api/address/customer/5
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<Address>>> GetAddressesByCustomerId(int customerId)
        {
            var addresses = await _addressService.GetAddressesByCustomerIdAsync(customerId);
            return Ok(addresses);
        }

        // POST: api/address
        [HttpPost]
        public async Task<ActionResult> AddAddress([FromBody] Address address)
        {
            var success = await _addressService.AddAddressAsync(address);
            if (success)
                return Ok(new { message = "Address added successfully" });

            return BadRequest(new { message = "Failed to add address" });
        }

        // PUT: api/address/customer/5
        [HttpPut("customer/{customerId}")]
        public async Task<ActionResult> UpdateAddressesByCustomerId(int customerId, [FromBody] List<Address> addresses)
        {
            var success = await _addressService.UpdateAddressesByCustomerIdAsync(customerId, addresses);
            if (success)
                return Ok(new { message = "Addresses updated successfully" });

            return BadRequest(new { message = "Failed to update addresses" });
        }

        // DELETE: api/address/customer/5
        [HttpDelete("customer/{customerId}")]
        public async Task<ActionResult> DeleteAddressesByCustomerId(int customerId)
        {
            var success = await _addressService.DeleteAddressesByCustomerIdAsync(customerId);
            if (success)
                return Ok(new { message = "Addresses deleted successfully" });

            return NotFound(new { message = $"No addresses found for customer ID {customerId}" });
        }
    }
}