using visus.Models.DTOs;

namespace visus.ApiService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Succeeded, string Token, string[] Errors)> LoginAsync(LoginDTO model);
        Task<(bool Succeeded, string[] Errors)> ForgotPasswordAsync(string email);
        Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(string userId, string token, string newPassword);
        Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}