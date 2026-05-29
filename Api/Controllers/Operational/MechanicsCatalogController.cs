using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Operational;

[ApiController]
[Route("api/mechanics-catalog")]
[Authorize(Roles = "Admin,WorkshopChief,Mechanic")]
public sealed class MechanicsCatalogController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public MechanicsCatalogController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("specialties")]
    public async Task<IActionResult> GetSpecialties(CancellationToken ct)
    {
        var specialties = await _dbContext.MechanicSpecialties
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new MechanicSpecialtyCatalogDto(x.Id, x.Name))
            .ToListAsync(ct);

        return Ok(specialties);
    }

    [HttpGet("mechanics")]
    public async Task<IActionResult> GetMechanics([FromQuery] int? specialtyId, [FromQuery] string? specialtyName, CancellationToken ct)
    {
        var query = _dbContext.MechanicSpecialtyAssignments
            .AsNoTracking()
            .Include(x => x.Person)
            .ThenInclude(x => x.User)
            .Include(x => x.Specialty)
            .Where(x => x.Person.User != null && x.Person.User.IsActive);

        if (specialtyId.HasValue)
        {
            query = query.Where(x => x.SpecialtyId == specialtyId.Value);
        }

        if (!string.IsNullOrWhiteSpace(specialtyName))
        {
            var normalized = specialtyName.Trim().ToLower();
            query = query.Where(x => x.Specialty.Name.ToLower() == normalized);
        }

        var rows = await query
            .OrderBy(x => x.Person.FirstName)
            .ThenBy(x => x.Person.LastName)
            .Select(x => new
            {
                x.PersonId,
                x.Person.FirstName,
                x.Person.MiddleName,
                x.Person.LastName,
                x.Person.SecondLastName,
                x.SpecialtyId,
                SpecialtyName = x.Specialty.Name
            })
            .ToListAsync(ct);

        var mechanics = rows
            .Select(x => new MechanicCatalogDto(
                x.PersonId,
                string.Join(" ", new[] { x.FirstName, x.MiddleName, x.LastName, x.SecondLastName }.Where(part => !string.IsNullOrWhiteSpace(part))),
                x.SpecialtyId,
                x.SpecialtyName))
            .ToList();

        return Ok(mechanics);
    }

    private sealed record MechanicSpecialtyCatalogDto(int Id, string Name);

    private sealed record MechanicCatalogDto(int PersonId, string FullName, int SpecialtyId, string SpecialtyName);
}
