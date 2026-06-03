// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con Audits. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.Controllers;
using Infrastructure.Context;
using Application.DTOs;
using Application.UseCase.Audits;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Audits;

[Authorize(Policy = "AdminOnly")]
public sealed class AuditsController : BaseApiController
{
    private readonly AppDbContext _context;

    public AuditsController(ISender sender, AppDbContext context) : base(sender)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var query = _context.Audits
            .AsNoTracking()
            .Include(audit => audit.AuditActionType)
            .Include(audit => audit.User).ThenInclude(user => user.Person)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(audit =>
                audit.AffectedEntity.ToLower().Contains(term) ||
                (audit.Description != null && audit.Description.ToLower().Contains(term)) ||
                audit.AuditActionType.Name.ToLower().Contains(term) ||
                audit.User.Person.FirstName.ToLower().Contains(term) ||
                audit.User.Person.LastName.ToLower().Contains(term));
        }

        var safePage = Math.Max(1, pageNumber);
        var safePageSize = Math.Max(1, pageSize);
        var total = await query.CountAsync(ct);
        var audits = await query
            .OrderByDescending(audit => audit.CreatedAt)
            .Skip((safePage - 1) * safePageSize)
            .Take(safePageSize)
            .ToListAsync(ct);

        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(audits.Select(audit => audit.ToDto()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetAuditById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterAudit command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/audits/{id}", new { id });
    }
}
