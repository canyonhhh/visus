using visus.Models.Entities;
using visus.Models.Enums;

namespace visus.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<(bool Succeeded, string[] Errors)> CreateAsync(User user, string password);
        Task<(bool Succeeded, string[] Errors)> UpdateAsync(User user);
        Task<(bool Succeeded, string[] Errors)> DeleteAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<(bool Succeeded, string[] Errors)> AddToRoleAsync(User user, UserRole role);
        Task<(bool Succeeded, string[] Errors)> RemoveFromRoleAsync(User user, UserRole role);
        Task<IList<string>> GetRolesAsync(User user);
        Task<bool> IsInRoleAsync(User user, UserRole role);
        Task<UserRole?> GetUserRoleAsync(User user);
    }
}