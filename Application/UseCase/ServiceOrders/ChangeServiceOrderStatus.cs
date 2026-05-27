using MediatR;

namespace Application.UseCase.ServiceOrders;

public sealed record ChangeServiceOrderStatus(int ServiceOrderId, int OrderStatusId, int UserId, string? Observation) : IRequest;
