using Api.Controllers;
using Application.UseCase.PartPurchaseDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PartPurchaseDetails;

public sealed class PartPurchaseDetailsController : BaseApiController
{
    public PartPurchaseDetailsController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPartPurchaseDetailsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) => Ok(await Sender.Send(new GetPartPurchaseDetailById(id), ct));
}
