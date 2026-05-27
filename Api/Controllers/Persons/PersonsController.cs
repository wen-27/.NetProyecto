using Api.Controllers;
using Api.DTOs.Persons;
using Application.UseCase.Persons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Persons;

public sealed class PersonsController : BaseApiController
{
    public PersonsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPersonsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPersonById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePerson command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/persons/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePersonRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePerson(id, request.FirstNames, request.LastNames), ct);
        return NoContent();
    }
}
