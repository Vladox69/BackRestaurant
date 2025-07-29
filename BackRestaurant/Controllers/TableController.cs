using BackRestaurant.Data;
using BackRestaurant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/table")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService) { 
            _tableService = tableService;
        }

        [HttpGet("business/{id}")]
        public async Task<IActionResult> GetTablesByBusinessId([FromRoute] int id)
        {
            try
            {
                var tables = await _tableService.GetTablesByBusinessId(id);
                return Ok(new { ok = true, data = tables, messages = "Fetching success" }); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
