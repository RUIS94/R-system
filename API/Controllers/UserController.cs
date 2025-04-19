using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DomainModels;
using Model.DTO;
using Service.Implementations;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserAsync(username);
            if (user == null)
            {
                return NotFound($"User with username {username} not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            bool success = await _userService.CreateUserAsync(user);
            if (!success)
            {
                return BadRequest("User creation failed due to business validation.");
            }

            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.UserName }, user);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, [FromBody] UpdateUserDto user)
        {
            if (user == null || username != user.UserName)
            {
                return BadRequest("User data is invalid.");
            }

            bool success = await _userService.UpdateUserAsync(user);
            if (!success)
            {
                return NotFound($"User with username {username} not found.");
            }

            return Ok(user);
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            bool success = await _userService.DeleteUserAsync(username);
            if (!success)
            {
                return NotFound($"User with username {username} not found.");
            }

            return NoContent();  // 204 No Content
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required");
            }

            bool isValid = await _userService.VerifyUserPasswordAsync(request.Username, request.Password);

            if (!isValid)
            {
                return Unauthorized("username or password is incorrect");
            }

            // 登录成功，这里可以生成 token 或 session（后续扩展）
            return Ok("Login successful");
        }
    }
}
