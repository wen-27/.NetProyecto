using MediatR;

namespace Application.UseCase.ServiceOrders;

// Caso de uso que modela una accion o consulta de negocio relacionada con ChangeServiceOrderStatus.
public sealed record ChangeServiceOrderStatus(int ServiceOrderId, int OrderStatusId, int UserId, string? Observation) : IRequest;
