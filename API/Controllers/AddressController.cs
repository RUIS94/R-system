using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Model.DTO;
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

        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        [HttpGet("customer/{username}")]
        public async Task<ActionResult<List<Address>>> GetAddressesByCustomer(string username)
        {
            var addresses = await _addressService.GetAddressesByCustomerAsync(username);
            return Ok(addresses);
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddAddress(string username, [FromBody] CreateAddressDto dto)
        {
            var success = await _addressService.AddAddressAsync(username, dto);
            if (success)
                return Ok(new { message = "Address added successfully" });

            return BadRequest(new { message = "Failed to add address" });
        }

        [HttpPut("customer/{username}")]
        public async Task<ActionResult> UpdateAddressesByCustomerId(string username, [FromBody] UpdateAddressDto dto)
        {
            var success = await _addressService.UpdateAddressesByCustomernameAsync(username, dto);
            if (success)
                return Ok(new { message = "Addresses updated successfully" });

            return BadRequest(new { message = "Failed to update addresses" });
        }

        [HttpDelete("customer/{username}")]
        public async Task<ActionResult> DeleteAddressesByCustomer(string username)
        {
            var success = await _addressService.DeleteAddressesByCustomerAsync(username);
            if (success)
                return Ok(new { message = "Addresses deleted successfully" });

            return NotFound(new { message = $"No addresses found for customer ID {username}" });
        }
    }
}