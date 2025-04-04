using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using visus.Data.Repositories.Interfaces;
using visus.Models.Entities;
using visus.Models.Enums;

namespace visus.Data.Repositories
{
    public class UserRepository(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        : IUserRepository
    {
        public async Task<User?> GetByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateAsync(User user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateAsync(User user)
        {
            var result = await userManager.UpdateAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteAsync(User user)
        {
            var result = await userManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<(bool Succeeded, string[] Errors)> AddToRoleAsync(User user, UserRole role)
        {
            await EnsureRoleExistsAsync(role);

            var roleName = RoleHelper.GetRoleName(role);
            var result = await userManager.AddToRoleAsync(user, roleName);

            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Succeeded, string[] Errors)> RemoveFromRoleAsync(User user, UserRole role)
        {
            var roleName = RoleHelper.GetRoleName(role);
            var result = await userManager.RemoveFromRoleAsync(user, roleName);

            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsInRoleAsync(User user, UserRole role)
        {
            var roleName = RoleHelper.GetRoleName(role);
            return await userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<UserRole?> GetUserRoleAsync(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
                return null;

            return RoleHelper.ParseRole(roles.First());
        }

        private async Task EnsureRoleExistsAsync(UserRole role)
        {
            string roleName = RoleHelper.GetRoleName(role);

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}