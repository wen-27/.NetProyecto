using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed record CreateServiceOrder(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate) : IRequest<int>;
