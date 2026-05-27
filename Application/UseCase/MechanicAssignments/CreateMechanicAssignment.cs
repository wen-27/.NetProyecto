using MediatR;

namespace Application.UseCase.MechanicAssignments;

public sealed record CreateMechanicAssignment(int OrderServiceId, int MechanicPersonId, int SpecialtyId) : IRequest<int>;
