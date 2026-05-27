using MediatR;

namespace Application.UseCase.OrderStatuses;

public sealed record UpdateOrderStatus(int Id, string Name) : IRequest;
