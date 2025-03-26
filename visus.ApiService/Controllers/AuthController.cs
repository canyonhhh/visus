using Microsoft.AspNetCore.Mvc;
using visus.ApiService.Services.Interfaces;
using visus.Models.DTOs;

namespace visus.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, errors) = await _authService.RegisterUserAsync(model);

            if (succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(new { errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, token, errors) = await _authService.LoginAsync(model);

            if (succeeded)
            {
                return Ok(new { token });
            }

            return Unauthorized(new { errors });
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _authService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}