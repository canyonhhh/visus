using Microsoft.EntityFrameworkCore;
using visus.Data.Contexts;
using visus.Data.Repositories.Interfaces;
using visus.Models.Entities;

namespace visus.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _context;

        public OrganizationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Organization?> GetByIdAsync(string id)
        {
            return await _context.Organizations
                .Include(o => o.Users)  // Include the related users if needed
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await _context.Organizations
                .Include(o => o.Users) // Include the related users if needed
                .ToListAsync();
        }

        public async Task CreateAsync(Organization organization)
        {
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Organization organization)
        {
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
        }
    }
}