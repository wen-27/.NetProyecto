using Api.DTOs.MechanicAssignments;
using Application.UseCase.CommonCrud;
using Application.UseCase.MechanicAssignments;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.MechanicAssignments;

public sealed class MechanicAssignmentsController : BaseApiController
{
    public MechanicAssignmentsController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetEntitiesPaged<MechanicAssignment>(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result.Items.Select(x => x.Adapt<MechanicAssignmentResponse>()).ToArray());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var entity = await Sender.Send(new GetEntityById<MechanicAssignment>(id), ct);
        return Ok(entity.Adapt<MechanicAssignmentResponse>());
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateMechanicAssignmentRequest request, CancellationToken ct)
    {
        var id = await Sender.Send(new CreateMechanicAssignment(request.OrderServiceId, request.MechanicPersonId, request.SpecialtyId), ct);
        return Created($"/api/mechanicassignments/{id}", new { id });
    }
}
