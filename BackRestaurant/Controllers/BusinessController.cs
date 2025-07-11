using BackRestaurant.Data;
using BackRestaurant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/business")]
    [ApiController]
    public class BusinessController : Controller
    {
        public readonly IBusinessService _businessService;
        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBusiness()
        {
            try
            {
                var business = await _businessService.GetAllBusiness();
                return Ok(business);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveBusiness([FromBody] Business business)
        {
            try
            {
                var result = await _businessService.SaveBusiness(business);
                if (!result)
                {
                    throw new Exception($"An error occurred while saving the business");
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
