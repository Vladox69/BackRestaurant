using BackRestaurant.Data;
using BackRestaurant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrderTransaction([FromBody] OrderBody body)
        {
            try
            {
                var result = await _orderService.CreateOrderTransaction(body)?? throw new Exception($"An error occurred while creating order");
                return Ok(new { ok = true, data = result, message = "Create success" });
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            try
            {
                var result = await _orderService.GetOrderById(id) ?? throw new Exception($"Not order found");
                return Ok(new { ok = true, data = result, message = "Fetching success" });
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

        [HttpGet("waiter-status")]
        public async Task<IActionResult> GetOrdersByIdWaiterAndStatus([FromQuery] int id, [FromQuery] string status)
        {
            try
            {
                var result = await _orderService.GetOrdersByIdWaiterAndStatus(id,status) ?? throw new Exception($"Not orders found");
                return Ok(new { ok = true, data = result, message = "Fetching success" });
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
