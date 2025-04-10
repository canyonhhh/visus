using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using visus.ApiService.Services.Interfaces;
using visus.Models.DTOs;

namespace visus.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAllOrganizations()
        {
            var organizations = await _organizationService.GetAllOrganizationsAsync();
            return Ok(organizations);
        }

        // GET: api/organization/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDto>> GetOrganizationById(string id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(organization);
        }

        // POST: api/organization
        [HttpPost]
        public async Task<ActionResult> CreateOrganization([FromBody] CreateOrganizationDto organizationDto)
        {
            if (organizationDto == null)
            {
                return BadRequest("Organization data is null.");
            }

            // Create the organization using the service
            var createdOrganization = await _organizationService.CreateOrganizationAsync(organizationDto);

            // Return CreatedAtAction with the id of the newly created organization
            return CreatedAtAction(
                nameof(GetOrganizationById),
                new { id = createdOrganization.Id },
                createdOrganization 
            );
        }


        // PUT: api/organization/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrganization(string id, [FromBody] UpdateOrganizationDto organizationDto)
        {
            if (organizationDto == null)
            {
                return BadRequest("Organization data is invalid.");
            }

            // Call the service method, passing the id from the route
            await _organizationService.UpdateOrganizationAsync(id, organizationDto);
    
            return NoContent();  // Return 204 No Content if the update is successful
        }


        // DELETE: api/organization/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrganization(string id)
        {
            var organization = await _organizationService.GetOrganizationByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            await _organizationService.DeleteOrganizationAsync(id);
            return NoContent();
        }
    }
}
