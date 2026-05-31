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
            .ToListAsync();

        return Ok(facilities.Select(ToResponse));
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
            .FirstOrDefaultAsync(f => f.Id == id);

        if (facility is null) return NotFound();
        return Ok(ToResponse(facility));
    }

    private static FacilityResponse ToResponse(Facility f) => new()
    {
        Id = f.Id,
        Code = f.Code,
        Name = f.Name,
        Latitude = f.Latitude,
        Longitude = f.Longitude,
        Address = f.Address,
        PostalCode = f.PostalCode,
        ContactNumber = f.ContactNumber,
        Email = f.Email,
        FacilityStatus = f.FacilityStatus,
        CreatedAt = f.CreatedAt,
        UpdatedAt = f.UpdatedAt,
        FacilityType = f.FacilityType is null ? null : new FacilityTypeResponse
        {
            Id = f.FacilityType.Id,
            Name = f.FacilityType.Name
        },
        Barangay = ToBarangayResponse(f.Barangay)
    };

    private static BarangayResponse? ToBarangayResponse(Barangay? b)
    {
        if (b is null) return null;
        return new BarangayResponse
        {
            Id = b.Id,
            Name = b.Name,
            LocalGovernmentUnit = ToLguResponse(b.LocalGovernmentUnit)
        };
    }

    private static LocalGovernmentUnitResponse? ToLguResponse(LocalGovernmentUnit? l)
    {
        if (l is null) return null;
        return new LocalGovernmentUnitResponse
        {
            Id = l.Id,
            Name = l.Name,
            Type = l.Type,
            Province = ToProvinceResponse(l.Province)
        };
    }

    private static ProvinceResponse? ToProvinceResponse(Province? p)
    {
        if (p is null) return null;
        return new ProvinceResponse
        {
            Id = p.Id,
            Name = p.Name,
            Region = ToRegionResponse(p.Region)
        };
    }

    private static RegionResponse? ToRegionResponse(Region? r)
    {
        if (r is null) return null;
        return new RegionResponse
        {
            Id = r.Id,
            Name = r.Name,
            CountryId = r.CountryId
        };
    }
}



