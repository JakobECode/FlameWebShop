using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Sms;

namespace WebApi.Controllers
{
    [UseApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _service;

        public SmsController(ISmsService service)
        {
            _service = service;
        }

        [Route("SendSms")]
        [HttpPost]
        public async Task<IActionResult> SendSms(SendSmsSchema schema)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.SendSmsAsync(schema.PhoneNumber, schema.Message);
                if (result)
                {
                    return Ok("Your message have been sent");
                }
                return Problem("Something went wrong on the server");
            }
            return BadRequest("You need to enter a valid phone number and a message");
        }
    }
}
