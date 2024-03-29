﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
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
            if (ModelState.IsValid)
            {
                var result = await _orderService.GetAllOrdersAsync();
                if (result != null)
                    return Ok(result);

                if (result == null)
                    return NotFound("No orders found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("GetByOrderId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.GetByOrderIdAsync(orderId);
                if (result != null)
                    return Ok(result);

                if (result == null)
                    return NotFound("No order found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("GetBySignedIn")]
        [HttpGet]
        public async Task<IActionResult> GetBySignedInUser()
        {
            if (ModelState.IsValid)
            {
                var userEmail = HttpContext.User.Identity!.Name;
                if (userEmail.IsNullOrEmpty())
                    return BadRequest("You must be signed in to use this method");

                var result = await _orderService.GetBySignedInUser(userEmail!);
                if (result != null)
                    return Ok(result);


                if (result == null)
                    return NotFound("No orders found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("GetByUserId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByUserId(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.GetByUserIdAsync(Id);
                if (result != null)
                    return Ok(result);

                if (result == null)
                    return NotFound("No orders found");
            }

            return BadRequest("Something went wrong, try again!");
        }


        [Route("CreateOrder")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateOrder(OrderSchema schema)
        {
            if (ModelState.IsValid)
            {
                var userEmail = HttpContext.User.Identity!.Name;
                var result = await _orderService.CreateOrderAsync(schema, userEmail!);
                if (result)
                    return Created("", null);
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("CancelOrder")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CancelOrder(OrderCancelSchema schema)
        {
            if (ModelState.IsValid)
            {
                var userEmail = HttpContext.User.Identity!.Name;
                if (userEmail != null)
                {
                    var result = await _orderService.CancelOrder(schema);
                    if (result)
                        return Ok("Order cancelled");
                }
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("DeleteOrder/{id}")]
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (ModelState.IsValid)
            {
                var userEmail = HttpContext.User.Identity!.Name;
                if (userEmail != null)
                {
                    var result = await _orderService.DeleteOrder(orderId);
                    if (result)
                        return Ok("Order deleted");
                }
            }

            return BadRequest("Something went wrong, try again!");
        }
    }
}
