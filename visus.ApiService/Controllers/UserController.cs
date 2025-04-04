using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using visus.ApiService.Services.Interfaces;
using visus.Models.DTOs;

namespace visus.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, errors) = await userService.RegisterUserAsync(model);

            if (succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(new { errors });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure the ID in the route matches the ID in the model
            if (id != model.Id)
            {
                return BadRequest(new { error = "ID mismatch" });
            }

            var (succeeded, errors) = await userService.UpdateUserAsync(id, model);

            if (succeeded)
            {
                return Ok(new { message = "User updated successfully" });
            }

            return BadRequest(new { errors });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var (succeeded, errors) = await userService.DeleteUserAsync(id);

            if (succeeded)
            {
                return Ok(new { message = "User deleted successfully" });
            }

            return BadRequest(new { errors });
        }

        [HttpPost("{id}/change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, errors) = await userService.ChangePasswordAsync(id, model.CurrentPassword, model.NewPassword);

            if (succeeded)
            {
                return Ok(new { message = "Password changed successfully" });
            }

            return BadRequest(new { errors });
        }

        [HttpPost("{id}/change-role")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeRole(string id, [FromBody] ChangeRoleDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (succeeded, errors) = await userService.ChangeUserRoleAsync(id, model.NewRole);

            if (succeeded)
            {
                return Ok(new { message = "Role changed successfully" });
            }

            return BadRequest(new { errors });
        }
    }
}