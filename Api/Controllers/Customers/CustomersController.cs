using Api.Controllers;
using Application.UseCase.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Customers;

public sealed class CustomersController : BaseApiController
{
    public CustomersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetCustomersPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetCustomerById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomer command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/customers/{id}", new { id });
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateCustomerStatusRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdateCustomer(id, request.Status), ct);
        return NoContent();
    }

    public sealed record UpdateCustomerStatusRequest(bool Status);
}
