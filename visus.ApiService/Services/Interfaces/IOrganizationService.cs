using visus.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using visus.Data.Migrations;

namespace visus.ApiService.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDto>> GetAllOrganizationsAsync();
        Task<OrganizationDto?> GetOrganizationByIdAsync(string id);
        Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationDto organizationDto);
        Task UpdateOrganizationAsync(string id, UpdateOrganizationDto organizationDto);
        Task DeleteOrganizationAsync(string id);
    }
}