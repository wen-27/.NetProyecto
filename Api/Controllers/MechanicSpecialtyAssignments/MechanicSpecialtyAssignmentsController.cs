using Api.DTOs.MechanicSpecialtyAssignments;
using Domain.Entities;
using MediatR;

namespace Api.Controllers.MechanicSpecialtyAssignments;

public sealed class MechanicSpecialtyAssignmentsController : CrudController<MechanicSpecialtyAssignment, CreateMechanicSpecialtyAssignmentRequest, UpdateMechanicSpecialtyAssignmentRequest, MechanicSpecialtyAssignmentResponse>
{
    public MechanicSpecialtyAssignmentsController(ISender sender) : base(sender) { }
}
