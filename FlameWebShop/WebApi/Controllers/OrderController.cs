using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;


using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [UseApiKey]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [Route("AllOrders")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            if (result != null)
                return Ok(result);

            return NotFound("No orders found");
        }

        [Route("GetByOrderId/{id}")] // Route template
        [HttpGet]
        public async Task<IActionResult> GetByOrderId(int id) // Parameter name matches route template
        {
            var result = await _orderService.GetByOrderIdAsync(id);
            if (result != null)
                return Ok(result);

            return NotFound("No order found");
        }

        //[Route("GetBySignedIn")]
        //[HttpGet]
        //public async Task<IActionResult> GetBySignedInUser()
        //{
        //    var userEmail = HttpContext.User.Identity!.Name;
        //    if (string.IsNullOrEmpty(userEmail))
        //        return BadRequest("You must be signed in to use this method");

        //    var result = await _orderService.GetBySignedInUser(userEmail!);
        //    if (result != null)
        //        return Ok(result);

        //    return NotFound("No orders found");
        //}

        [Route("GetByUserId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByUserId(int Id)
        {
            var result = await _orderService.GetByOrderIdAsync(Id);
            if (result != null)
                return Ok(result);

            return NotFound("No orders found");
        }


        [Route("CreateOrder")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateOrder(OrderDto schema)
        {
            var userEmail = HttpContext.User.Identity!.Name;
            userEmail = "Test@gmail.com";
            var result = await _orderService.CreateOrderAsync(schema, userEmail);
            if (result)
                return Created("", null); // Ideally, you should include a URI to the newly created resource here

            return BadRequest("Something went wrong, try again!");
        }

        [Route("CancelOrder")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CancelOrder(OrderCancelSchema schema)
        {
            var userEmail = HttpContext.User.Identity!.Name;
            if (userEmail != null)
            {
                var result = await _orderService.CancelOrder(schema);
                if (result)
                    return Ok("Order cancelled");
            }
            else
            {
                return BadRequest("User must be signed in to cancel orders.");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("DeleteOrder/{id}")]
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var userEmail = HttpContext.User.Identity!.Name;
            if (userEmail != null)
            {
                var result = await _orderService.DeleteOrder(orderId);
                if (result)
                    return Ok("Order deleted");
                else
                    return NotFound("Order not found");
            }
            else
            {
                return BadRequest("User must be signed in to delete orders.");
            }
        }
    }
}
