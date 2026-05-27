using Api.Controllers;
using Application.UseCase.Parts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Parts;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreatePart command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/parts/{id}", new { id });
    }

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

    public sealed record UpdatePartRequest(
        int PartCategoryId,
        int? PartBrandId,
        string Code,
        string Description,
        int Stock,
        int MinimumStock,
        decimal UnitPrice,
        bool IsActive);
}
