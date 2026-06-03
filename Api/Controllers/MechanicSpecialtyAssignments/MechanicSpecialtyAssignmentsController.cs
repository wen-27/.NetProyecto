using Api.DTOs.MechanicSpecialtyAssignments;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.MechanicSpecialtyAssignments;

// Controlador encargado de exponer por HTTP las operaciones relacionadas con MechanicSpecialtyAssignments.
public sealed class MechanicSpecialtyAssignmentsController : CrudController<MechanicSpecialtyAssignment, CreateMechanicSpecialtyAssignmentRequest, UpdateMechanicSpecialtyAssignmentRequest, MechanicSpecialtyAssignmentResponse>
{
    // Las acciones de este controlador deben delegar reglas de negocio a Application o servicios especializados.
    public MechanicSpecialtyAssignmentsController(ISender sender) : base(sender) { }
}
