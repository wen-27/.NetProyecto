using Api.Controllers;
using Api.DTOs.Parts;
using Application.UseCase.Parts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers.Parts;

[EnableRateLimiting("parts")]
public sealed class PartsController : BaseApiController
{
    public PartsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPartsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPartById(id), ct));
    }

    [Authorize(Policy = "InventoryManagerOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePart command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/parts/{id}", new { id });
    }

    [Authorize(Policy = "InventoryManagerOrAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePartRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePart(
            id,
            request.PartCategoryId,
            request.PartBrandId,
            request.Code,
            request.Description,
            request.Stock,
            request.MinimumStock,
            request.UnitPrice,
            request.IsActive), ct);

        return NoContent();
    }
}
