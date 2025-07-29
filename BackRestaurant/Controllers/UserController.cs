using BackRestaurant.Models;
using BackRestaurant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackRestaurant.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(new {ok=true,data=users,message="Fetching success"});
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginForm form)
        {
            try
            {
                var result = await _userService.Login(form);
                return Ok(new {ok=true,data= result,message="Login success"});
            }
            catch (Exception ex)
            {
                return StatusCode(500,new
                {
                    ok = false,
                    message = $"Internal server error: {ex.Message}"
                });
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveUser([FromBody] User user)
        {
            try
            {
                var result = await _userService.SaveUser(user);
                if (!result)
                {
                    throw new Exception($"An error occurred while saving the user");
                }
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
