using MediatR;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateServiceOrder.
public sealed record CreateServiceOrder(
    int VehicleId,
    int OrderStatusId,
    DateTime? EstimatedDeliveryDate) : IRequest<int>;
