using Api.Controllers;
using Application.UseCase.PaymentCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PaymentCards;

public sealed class PaymentCardsController : BaseApiController
{
    public PaymentCardsController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetPaymentCardsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) => Ok(await Sender.Send(new GetPaymentCardById(id), ct));
}
