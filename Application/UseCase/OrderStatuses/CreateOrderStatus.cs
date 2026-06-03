using MediatR;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateOrderStatus.
public sealed record CreateOrderStatus(string Name) : IRequest<int>;
