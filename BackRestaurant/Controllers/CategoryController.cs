using BackRestaurant.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();
                return Ok(new { ok = true, data = categories, messages = "Fetching success" }); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
