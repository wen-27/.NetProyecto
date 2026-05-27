using MediatR;

namespace Application.UseCase.OrderStatuses;

public sealed record CreateOrderStatus(string Name) : IRequest<int>;
