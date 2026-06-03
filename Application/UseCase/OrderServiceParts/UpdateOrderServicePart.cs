using MediatR;

namespace Application.UseCase.OrderServiceParts;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateOrderServicePart.
public sealed record UpdateOrderServicePart(int Id, int Quantity, decimal AppliedUnitPrice, bool? CustomerApproved = null) : IRequest;
