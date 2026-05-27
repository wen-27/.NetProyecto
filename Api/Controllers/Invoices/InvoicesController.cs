using Api.Controllers;
using Application.UseCase.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Invoices;

public sealed class InvoicesController : BaseApiController
{
    public InvoicesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetInvoicesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetInvoiceById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Generate(GenerateInvoice command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/invoices/{id}", new { id });
    }
}
