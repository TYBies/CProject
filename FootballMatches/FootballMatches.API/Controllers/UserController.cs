using Microsoft.AspNetCore.Mvc;
using FootballMatches.API.DTOs;

namespace FootballMatches.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // POST api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.CreateUserAsync(userDto.Username, userDto.Password);
                _logger.LogInformation("User registered successfully with username: {Username}", userDto.Username);
                return Ok(new { message = "User registered successfully." });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Username already exists: {Username}", userDto.Username);
                return Conflict(new { message = "Username already exists." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user with username: {Username}", userDto.Username);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _userService.AuthenticateUserAsync(userDto.Username, userDto.Password);
                _logger.LogInformation("User authenticated successfully with username: {Username}", userDto.Username);
                return Ok(new{ Token = token, message = "Login successful" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Invalid login attempt for username: {Username}", userDto.Username);
                return Unauthorized(new { message = "Invalid username or password." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating the user with username: {Username}", userDto.Username);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

    }
}
