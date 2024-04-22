using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    //[UseApiKey]
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        [Route("RegisterAddress")]
        public async Task<IActionResult> RegisterAddress(RegisterAddressSchema schema)
        {
            var userName = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                if (await _addressService.RegisterAddressAsync(schema, userName))
                {
                    return Created("", null);  // Ideally, you should include a URI to the newly registered address here.
                }
                else
                {
                    return BadRequest("Failed to register address.");
                }
            }
            else
            {
                return BadRequest("User must be signed in to register an address.");
            }
        }

        [HttpPut]
        [Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressSchema schema)
        {
            var userName = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                var result = await _addressService.UpdateAddressAsync(schema, userName);
                if (result != null)
                {
                    return Ok("Address updated");
                }
                else
                {
                    return NotFound("Address not found or update failed.");
                }
            }
            else
            {
                return BadRequest("User must be signed in to update an address.");
            }
        }

        [HttpGet]
        [Route("GetUserAddresses")]
        public async Task<IActionResult> GetUserAddresses()
        {
            var userName = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                var result = await _addressService.GetUserAddressesAsync(userName);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No addresses found.");
            }
            else
            {
                return BadRequest("User must be signed in to access addresses.");
            }
        }

        [HttpDelete]
        [Route("RemoveAddress/{id}")]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            var result = await _addressService.DeleteAddressAsync(id);
            if (result)
                return Ok("Address removed");
            else
                return NotFound("Address not found.");
        }

    }
}
