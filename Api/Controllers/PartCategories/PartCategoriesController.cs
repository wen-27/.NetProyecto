using Api.Controllers;
using Api.DTOs.PartCategories;
using Application.UseCase.PartCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PartCategories;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con PartCategories.
public sealed class PartCategoriesController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public PartCategoriesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPartCategoriesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPartCategoryById(id), ct));
    }

    [Authorize(Policy = "InventoryManagerOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePartCategory command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/partcategories/{id}", new { id });
    }

    [Authorize(Policy = "InventoryManagerOrAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePartCategoryRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePartCategory(id, request.Name), ct);
        return NoContent();
    }
}
