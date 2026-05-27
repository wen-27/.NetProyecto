using Api.Controllers;
using Api.DTOs.ServiceTypes;
using Application.UseCase.ServiceTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ServiceTypes;

public sealed class ServiceTypesController : BaseApiController
{
    public ServiceTypesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetServiceTypesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetServiceTypeById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceType command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/servicetypes/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateServiceTypeRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateServiceType(id, request.Name, request.EstimatedDays), ct);
        return NoContent();
    }
}
