using Microsoft.AspNetCore.Identity;
using visus.ApiService.Services.Interfaces;
using visus.Data.Repositories.Interfaces;
using visus.Models.DTOs;
using visus.Models.Entities;
using visus.Models.Enums;

namespace visus.ApiService.Services
{
    public class UserService(
        IUserRepository userRepository,
        UserManager<User> userManager)
        : IUserService
    {
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            var userDtos = new List<UserDTO>();

            foreach (var user in users)
            {
                var role = await userRepository.GetUserRoleAsync(user);
                userDtos.Add(new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    Role = role ?? UserRole.STAFF
                });
            }

            return userDtos;
        }

        public async Task<UserDTO?> GetUserByIdAsync(string id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            var role = await userRepository.GetUserRoleAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Role = role ?? UserRole.STAFF
            };
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if (user == null)
                return null;

            var role = await userRepository.GetUserRoleAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Role = role ?? UserRole.STAFF
            };
        }

        public async Task<(bool Succeeded, string[] Errors)> RegisterUserAsync(RegisterDTO model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = UserRole.STAFF // Default role
            };

            var (succeeded, errors) = await userRepository.CreateAsync(user, model.Password);

            if (succeeded)
            {
                // Assign the default role
                await userRepository.AddToRoleAsync(user, UserRole.STAFF);
            }

            return (succeeded, errors);
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(string id, UserDTO model)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return (false, ["User not found"]);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email; // Keep username and email in sync

            // Update role if changed
            var currentRole = await userRepository.GetUserRoleAsync(user);
            if (currentRole == model.Role)
            {
                return await userRepository.UpdateAsync(user);
            }

            if (currentRole.HasValue)
            {
                await userRepository.RemoveFromRoleAsync(user, currentRole.Value);
            }
            await userRepository.AddToRoleAsync(user, model.Role);
            user.Role = model.Role;

            return await userRepository.UpdateAsync(user);
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return (false, ["User not found"]);

            return await userRepository.DeleteAsync(user);
        }

        public async Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(string id, string currentPassword, string newPassword)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return (false, ["User not found"]);

            var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string[] Errors)> ChangeUserRoleAsync(string id, UserRole newRole)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                return (false, ["User not found"]);

            // Remove from current roles
            var currentRoles = await userRepository.GetRolesAsync(user);
            foreach (var roleName in currentRoles)
            {
                var role = RoleHelper.ParseRole(roleName);
                if (role.HasValue)
                {
                    await userRepository.RemoveFromRoleAsync(user, role.Value);
                }
            }

            // Add to new role
            user.Role = newRole;
            var result = await userRepository.AddToRoleAsync(user, newRole);

            if (result.Succeeded)
            {
                await userRepository.UpdateAsync(user);
            }

            return result;
        }
    }
}