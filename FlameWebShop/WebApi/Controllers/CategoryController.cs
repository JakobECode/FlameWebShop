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
            var result = await _categoryService.GetAllAsync();
            if (result != null)
                return Ok(result);
            else
                return NotFound("No categories found");
        }

        [Route("GetCategoryById")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound("No category found");
        }

        [Route("AddCategory")]
        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> AddCategory(CategorySchema schema)
        {
            var result = await _categoryService.CreateAsync(schema);
            if (result)
                return Created("", null);  // Ideally, you should include a URI to the newly created category here.
            else
                return BadRequest("Failed to add category.");
        }

        [Route("DeleteCategory/{id}")]
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userName = HttpContext.User.Identity!.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                var result = await _categoryService.DeleteAsync(id);
                if (result)
                    return Ok("Category deleted");
                else
                    return NotFound("Category not found.");
            }
            else
            {
                return BadRequest("User must be signed in to delete a category.");
            }
        }
    }
}
