using BackRestaurant.Data;
using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/order-item")]
    [ApiController]
    public class OrderItemController : Controller
    {
        public readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult>  GetOrderItemsByOrderId([FromRoute] int id)
        {
            try
            {
                var res = await _orderItemService.GetOrderItemsByOrderId(id);
                return Ok(new { ok = true, data = res, message = "Fetching success" });
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
        public async Task<IActionResult> SaveOrderItem([FromBody] OrderItem orderItem)
        {
            try
            {
                var result = await _orderItemService.SaveOrderItem(orderItem);
                if (!result)
                {
                    throw new Exception($"An error occurred while saving the business");
                }
                return Ok(new { ok = true, data = result, message = "Fetching success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
