using Application.UseCase.CommonCrud;
using Domain.Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con Crud.
public abstract class CrudController<TEntity, TCreateRequest, TUpdateRequest, TResponse> : BaseApiController
    where TEntity : BaseEntity, new()
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    protected CrudController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery(Name = "pageNumber")] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetEntitiesPaged<TEntity>(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();

        return Ok(new
        {
            items = result.Items.Select(x => x.Adapt<TResponse>()).ToArray(),
            result.TotalCount,
            result.Page,
            result.PageSize,
            result.TotalPages,
            result.HasPreviousPage,
            result.HasNextPage
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var entity = await Sender.Send(new GetEntityById<TEntity>(id), ct);
        return Ok(entity.Adapt<TResponse>());
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(TCreateRequest request, CancellationToken ct)
    {
        var entity = request.Adapt<TEntity>()
            ?? throw new InvalidOperationException($"No se pudo mapear {typeof(TCreateRequest).Name} a {typeof(TEntity).Name}.");
        var id = await Sender.Send(new CreateEntity<TEntity>(entity), ct);
        return Created($"{Request.Path}/{id}", new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TUpdateRequest request, CancellationToken ct)
    {
        var entity = request.Adapt<TEntity>()
            ?? throw new InvalidOperationException($"No se pudo mapear {typeof(TUpdateRequest).Name} a {typeof(TEntity).Name}.");
        await Sender.Send(new UpdateEntity<TEntity>(id, entity), ct);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Sender.Send(new DeleteEntity<TEntity>(id), ct);
        return NoContent();
    }
}
