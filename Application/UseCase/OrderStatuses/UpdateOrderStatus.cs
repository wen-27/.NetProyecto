using MediatR;

namespace Application.UseCase.OrderStatuses;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateOrderStatus.
public sealed record UpdateOrderStatus(int Id, string Name) : IRequest;
