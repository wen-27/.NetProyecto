// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con WorkshopServices. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Application.Abstractions.OperationalWorkflow;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Operational;

[Route("api/workshop-services")]
public sealed class WorkshopServicesController : OperationalControllerBase
{
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
