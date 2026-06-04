using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;

namespace TeamMapSyncApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FacilityController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FacilityResponse>>> GetAll()
    {
        var facilities = await db.Facilities
            .Include(f => f.FacilityType)
            .Include(f => f.Barangay)
                .ThenInclude(b => b!.LocalGovernmentUnit)
                    .ThenInclude(lgu => lgu!.Province)
                        .ThenInclude(p => p!.Region)
            .AsNoTracking()
            .ToListAsync(cancellationToken: HttpContext.RequestAborted);

        return Ok(facilities.Select(FacilityResponse.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FacilityResponse>> GetById(Guid id)
    {
        var facility = await db.Facilities
            .Include(f => f.FacilityType)
            .Include(f => f.Barangay)
                .ThenInclude(b => b!.LocalGovernmentUnit)
                    .ThenInclude(lgu => lgu!.Province)
                        .ThenInclude(p => p!.Region)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken: HttpContext.RequestAborted);

        if (facility is null) return NotFound();
        return Ok(FacilityResponse.ToResponse(facility));
    }
}