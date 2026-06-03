using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[Route("api/workshop-services")]
// Controlador encargado de exponer por HTTP las operaciones relacionadas con WorkshopServices.
public sealed class WorkshopServicesController : OperationalControllerBase
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    private readonly IOperationalWorkflowService _workflow;

    public WorkshopServicesController(IOperationalWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpGet]
    [Authorize]
    public Task<IActionResult> Get(CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWorkshopServicesAsync(ct)));

    [HttpGet("{id:int}")]
    [Authorize]
    public Task<IActionResult> GetById(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.GetWorkshopServiceAsync(id, ct)));

    [HttpPost]
    [Authorize(Roles = "WorkshopChief,Admin")]
    public Task<IActionResult> Create(CreateWorkshopServiceDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Created("/api/workshop-services", await _workflow.CreateWorkshopServiceAsync(dto, ct)));

    [HttpPut("{id:int}")]
    [Authorize(Roles = "WorkshopChief,Admin")]
    public Task<IActionResult> Update(int id, UpdateWorkshopServiceDto dto, CancellationToken ct) =>
        ExecuteAsync(async () => Ok(await _workflow.UpdateWorkshopServiceAsync(id, dto, ct)));

    [HttpPatch("{id:int}/activate")]
    [Authorize(Roles = "WorkshopChief,Admin")]
    public Task<IActionResult> Activate(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.SetWorkshopServiceStatusAsync(id, true, ct)));

    [HttpPatch("{id:int}/deactivate")]
    [Authorize(Roles = "WorkshopChief,Admin")]
    public Task<IActionResult> Deactivate(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await _workflow.SetWorkshopServiceStatusAsync(id, false, ct)));
}
