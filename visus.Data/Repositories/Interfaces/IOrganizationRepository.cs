using visus.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace visus.Data.Repositories.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(string id);
        Task<IEnumerable<Organization>> GetAllAsync();
        Task CreateAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
    }
}