using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using visus.ApiService.Services.Interfaces;
using visus.Data.Repositories.Interfaces;
using visus.Models.DTOs;
using visus.Models.Entities;
using visus.Models.Enums;

namespace visus.ApiService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<(bool Succeeded, string Token, string[] Errors)> LoginAsync(LoginDTO model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);

            if (user == null)
            {
                return (false, string.Empty, new[] { "User does not exist" });
            }

            var result = await _userRepository.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return (false, string.Empty, new[] { "Invalid password" });
            }

            var token = await GenerateJwtToken(user);

            return (true, token, Array.Empty<string>());
        }

        public async Task<(bool Succeeded, string[] Errors)> ForgotPasswordAsync(string email)
        {
            // TODO : Implement email sending logic
            throw new NotImplementedException("Email sending is not implemented yet.");
            
/*
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return (true, Array.Empty<string>());
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            return (true, Array.Empty<string>());
*/
        }

        public async Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return (false, new[] { "User not found" });
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return (false, new[] { "User not found" });
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var userRole = await _userRepository.GetUserRoleAsync(user);
            var roleName = userRole.HasValue ? RoleHelper.GetRoleName(userRole.Value) : string.Empty;

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrEmpty(roleName))
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            }

            if (!string.IsNullOrEmpty(user.LastName))
            {
                claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ??
                                                                      throw new ArgumentException("JWT:Secret is not configured")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpirationInDays"] ??
                                                            throw new ArgumentException("JWT:ExpirationInDays is not configured")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}