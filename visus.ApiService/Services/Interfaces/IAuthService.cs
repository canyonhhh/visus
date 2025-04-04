using visus.Models.DTOs;
using visus.Models.Enums;

namespace visus.ApiService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(RegisterDTO model);
        Task<(bool Succeeded, string Token, string[] Errors)> LoginAsync(LoginDTO model);
        Task<UserDTO?> GetUserByEmailAsync(string email);
        Task<bool> AssignRoleToUserAsync(string userId, UserRole role);
        Task<UserRole> GetUserRoleAsync(string userId);
    }
}