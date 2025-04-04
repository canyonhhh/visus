using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using visus.ApiService.Services.Interfaces;
using visus.Models.DTOs;

namespace visus.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, token, errors) = await authService.LoginAsync(model);

            if (succeeded)
            {
                return Ok(new { token });
            }

            return Unauthorized(new { errors });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await authService.ForgotPasswordAsync(model.Email);

            return Ok(new { message = "If your email is registered, you will receive a password reset link shortly." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, errors) = await authService.ResetPasswordAsync(model.UserId, model.Token, model.NewPassword);

            if (succeeded)
            {
                return Ok(new { message = "Password has been reset successfully." });
            }

            return BadRequest(new { errors });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var (succeeded, errors) = await authService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);

            if (succeeded)
            {
                return Ok(new { message = "Password changed successfully." });
            }

            return BadRequest(new { errors });
        }
    }
}