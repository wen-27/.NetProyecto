using Api.Controllers;
using Api.DTOs.PersonPhones;
using Application.UseCase.PersonPhones;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PersonPhones;

public sealed class PersonPhonesController : BaseApiController
{
    public PersonPhonesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPersonPhonesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPersonPhoneById(id), ct));
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Add(AddPersonPhone command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/personphones/{id}", new { id });
    }

    [Authorize(Policy = "ReceptionistOrAdmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePersonPhoneRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePersonPhone(id, request.CountryId, request.PhoneNumber, request.IsPrimary), ct);
        return NoContent();
    }
}
