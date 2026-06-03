using MediatR;

namespace Application.UseCase.OrderServiceParts;

// Caso de uso que modela una accion o consulta de negocio relacionada con DeleteOrderServicePart.
public sealed record DeleteOrderServicePart(int Id) : IRequest;
