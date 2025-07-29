using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) { 
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var categories = await _productService.GetAllProducts();
                return Ok(new {ok=true,data= categories ,messages= "Fetching success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("business/{id}")]
        public async Task<IActionResult> GetProductsByBusinessId([FromRoute] int id)
        {
            try
            {
                var products = await _productService.GetProductsByBusinessId(id);
                return Ok(new { ok=true,data=products, message = "Fetching success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ok = false,
                    message = $"Internal server error: {ex.Message}"
                });
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveProduct([FromBody] Product product)
        {
            try
            {
                var result = await _productService.SaveProduct(product);
                if (!result)
                {
                    throw new Exception($"An error occurred while saving the product");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
