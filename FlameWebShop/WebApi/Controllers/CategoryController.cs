using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Filters;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;

namespace WebApi.Controllers
{
    [UseApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("GetAllCategories")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.GetAllAsync();
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No categories found");
            }

            return BadRequest("Something went wrong, try again");
        }

        [Route("GetCategoryById")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.GetByIdAsync(id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No category found");
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("AddCategory")]
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> AddCategory(CategorySchema schema)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.CreateAsync(schema);
                if (result)
                    return Created("", null);
            }

            return BadRequest("Something went wrong, try again!");
        }

        [Route("DeleteCategory/{id}")]
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity!.Name;
                if (userName != null)
                {
                    var result = await _categoryService.DeleteAsync(id);
                    if (result)
                        return Ok("Category deleted");
                }
            }

            return BadRequest("Something went wrong, try again!");
        }
    }
}
