using Api.DTOs.Countries;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Countries;

[ApiController]
[Route("api/[controller]")]
[Authorize]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con Countries.
public sealed class CountriesController : ControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly AppDbContext _context;

    public CountriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CountryResponse>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 100,
        [FromQuery] string? search = null,
        CancellationToken cancellationToken = default)
    {
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = Math.Clamp(pageSize, 1, 200);

        var query = _context.Countries.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(country =>
                country.Name.Contains(term) ||
                (country.PhoneCode != null && country.PhoneCode.Contains(term)));
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(country => country.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(country => new CountryResponse(country.Id, country.Name, country.PhoneCode))
            .ToListAsync(cancellationToken);

        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var item = await _context.Countries
            .AsNoTracking()
            .Where(country => country.Id == id)
            .Select(country => new CountryResponse(country.Id, country.Name, country.PhoneCode))
            .FirstOrDefaultAsync(cancellationToken);

        return item is null ? NotFound() : Ok(item);
    }
}
