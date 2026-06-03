using Api.DTOs.Genders;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Genders;

[ApiController]
[Route("api/[controller]")]
[Authorize]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con Genders.
public sealed class GendersController : ControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly AppDbContext _context;

    public GendersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GenderResponse>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string? search = null,
        CancellationToken cancellationToken = default)
    {
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = Math.Clamp(pageSize, 1, 200);

        var query = _context.Genders.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(gender => gender.Name.Contains(term));
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(gender => gender.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(gender => new GenderResponse(gender.Id, gender.Name))
            .ToListAsync(cancellationToken);

        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenderResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var item = await _context.Genders
            .AsNoTracking()
            .Where(gender => gender.Id == id)
            .Select(gender => new GenderResponse(gender.Id, gender.Name))
            .FirstOrDefaultAsync(cancellationToken);

        return item is null ? NotFound() : Ok(item);
    }
}
