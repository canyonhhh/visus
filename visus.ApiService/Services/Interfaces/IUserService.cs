using visus.Models.DTOs;
using visus.Models.Entities;
using visus.Models.Enums;

namespace visus.ApiService.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(string id);
        Task<UserDTO?> GetUserByEmailAsync(string email);
        Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(RegisterDTO model);
        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(string id, UserDTO model);
        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string id);
        Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(string id, string currentPassword, string newPassword);
        Task<(bool Succeeded, string[] Errors)> ChangeUserRoleAsync(string id, UserRole newRole);
    }
}