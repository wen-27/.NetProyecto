using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed record UpdateServiceOrderService(
    int Id,
    int ServiceTypeId,
    int MechanicId,
    string? Description,
    decimal LaborCost) : IRequest;
