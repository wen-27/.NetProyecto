using Api.Controllers;
using Application.UseCase.PhoneCodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PhoneCodes;

public sealed class PhoneCodesController : BaseApiController
{
    public PhoneCodesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPhoneCodesPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        return Ok(await Sender.Send(new GetPhoneCodeById(id), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePhoneCode command, CancellationToken ct)
    {
        var id = await Sender.Send(command, ct);
        return Created($"/api/phonecodes/{id}", new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdatePhoneCodeRequest request, CancellationToken ct)
    {
        await Sender.Send(new UpdatePhoneCode(id, request.Code, request.Country), ct);
        return NoContent();
    }

    public sealed record UpdatePhoneCodeRequest(string Code, string Country);
}
