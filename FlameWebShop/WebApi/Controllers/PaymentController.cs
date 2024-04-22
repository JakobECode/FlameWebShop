using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    //[Authorize]
    //[UseApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("GetUserCreditCards")]
        public async Task<IActionResult> GetUserCreditCards()
        {
            var userEmail = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var result = await _paymentService.GetUserCreditCardsAsync(userEmail);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No credit cards found.");
            }
            else
            {
                return BadRequest("User must be signed in to access credit cards.");
            }
        }

        [HttpPost]
        [Route("RegisterCreditCard")]
        public async Task<IActionResult> RegisterCreditCard(RegisterCreditCardSchema schema)
        {
            var userEmail = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var result = await _paymentService.RegisterCreditCardsAsync(schema, userEmail);
                if (result)
                    return Created("", null); // Ideally, you should include a URI to the newly registered credit card here
                else
                    return BadRequest("Registration failed.");
            }
            else
            {
                return BadRequest("User must be signed in to register a credit card.");
            }
        }

        [HttpDelete]
        [Route("RemoveCreditCard/{id}")]
        public async Task<IActionResult> RemoveCreditCard(int id)
        {
            var userEmail = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var result = await _paymentService.DeleteCreditCardsAsync(id, userEmail);
                if (result)
                    return Ok("Credit card removed successfully.");
                else
                    return NotFound("Credit card not found.");
            }
            else
            {
                return BadRequest("User must be signed in to remove a credit card.");
            }
        }
    }
}
