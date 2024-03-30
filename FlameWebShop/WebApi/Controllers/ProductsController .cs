using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    //[UseApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.GetAllAsync();
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No products found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.GetByIdAsync(id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No product found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("Category")]
        [HttpGet]
        public async Task<IActionResult> GetByCategory(string category)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.GetByCategoryAsync(category);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No products found");
            }

            return BadRequest("Sometihing went wrong, try again!");
        }

        [Route("Price")]
        [HttpGet]
        public async Task<IActionResult> GetByPrice(int minPrice, int maxPrice)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.GetByPriceAsync(minPrice, maxPrice);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No products found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("Search")]
        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _productService.GetByNameAsync(name);
            if (result != null)
                return Ok(result);

            return NotFound("No products found");
        }

        [Route("Add")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddProduct(ProductSchema schema)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateAsync(schema);
                if (result)
                    return Created("", null);
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("Delete/{id}")]
        [HttpDelete]
       // [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity!.Name;
                if (userName != null)
                {
                    var result = await _productService.DeleteAsync(id);
                    if (result)
                        return Ok("Product deleted");
                }
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("Update")]
        [HttpPut]
       // [Authorize]
        public async Task<IActionResult> UpdateProduct(ProductSchema schema)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity!.Name;
                if (userName != null)
                {
                    var result = await _productService.UpdateAsync(schema);
                    if (result)
                        return Ok("Product updated");
                }
            }

            return BadRequest("Something went wrong, try again!");
        }
    }
}
