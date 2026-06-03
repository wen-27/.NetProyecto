// Responsabilidad: Controlador HTTP que expone endpoints REST relacionados con MechanicSpecialtyAssignments. Coordina validacion de entrada, autorizacion y delega la logica a Application/Infrastructure.
// Nota de mantenimiento: No debe contener reglas de negocio extensas; esas reglas pertenecen a Application o servicios especializados.
using Api.DTOs.MechanicSpecialtyAssignments;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.MechanicSpecialtyAssignments;

public sealed class MechanicSpecialtyAssignmentsController : CrudController<MechanicSpecialtyAssignment, CreateMechanicSpecialtyAssignmentRequest, UpdateMechanicSpecialtyAssignmentRequest, MechanicSpecialtyAssignmentResponse>
{
    public MechanicSpecialtyAssignmentsController(ISender sender) : base(sender) { }
}
