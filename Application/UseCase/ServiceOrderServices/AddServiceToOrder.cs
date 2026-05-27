using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed record AddServiceToOrder(
    int ServiceOrderId,
    int ServiceTypeId,
    int MechanicId,
    string? Description,
    decimal LaborCost) : IRequest<int>;
