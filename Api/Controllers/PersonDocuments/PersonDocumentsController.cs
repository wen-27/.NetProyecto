using Api.Controllers;
using Application.UseCase.PersonDocuments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PersonDocuments;

public sealed class PersonDocumentsController : BaseApiController
{
    public PersonDocumentsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPersonDocumentsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPersonDocumentById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddPersonDocument command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/persondocuments/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePersonDocumentRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePersonDocument(id, request.DocumentTypeId, request.DocumentNumber, request.IsPrimary), ct);
        return NoContent();
    }

    public sealed record UpdatePersonDocumentRequest(int DocumentTypeId, string DocumentNumber, bool IsPrimary);
}
