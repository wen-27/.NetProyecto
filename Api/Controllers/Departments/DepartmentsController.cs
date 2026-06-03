using Api.Controllers;
using Application.UseCase.Departments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Departments;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con Departments.
public sealed class DepartmentsController : BaseApiController
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public DepartmentsController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery(Name = "pageNumber")] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await Sender.Send(new GetDepartmentsPaged(pageNumber, pageSize, search), ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct) => Ok(await Sender.Send(new GetDepartmentById(id), ct));
}
