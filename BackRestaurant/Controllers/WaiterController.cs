using BackRestaurant.Data;
using BackRestaurant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/waiter")]
    [ApiController]
    public class WaiterController : Controller
    {
        private readonly IWaiterService _waiterService;
        public WaiterController(IWaiterService waiterService)
        {
            _waiterService = waiterService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWaiters()
        {
            try
            {
                var waiters = await _waiterService.GetAllWaiters();
                return Ok(waiters);
            }catch (Exception ex){
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveWaiter([FromBody] Waiter waiter)
        {
            try
            {
                var result = await _waiterService.SaveWaiter(waiter);
                if (!result)
                {
                    throw new Exception($"An error occurred while saving the waiter");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWaiterById([FromRoute] int id)
        {
            try
            {
                var waiter = await _waiterService.GetWaiterById(id);
                if (waiter == null)
                {
                    return NotFound($"No waiter found with id {id}");
                }
                return Ok(new{ok=true,data=waiter, message = "Fetching success" });
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
    }
}
