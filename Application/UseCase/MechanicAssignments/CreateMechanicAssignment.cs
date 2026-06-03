using MediatR;

namespace Application.UseCase.MechanicAssignments;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateMechanicAssignment.
public sealed record CreateMechanicAssignment(int OrderServiceId, int MechanicPersonId, int SpecialtyId) : IRequest<int>;
