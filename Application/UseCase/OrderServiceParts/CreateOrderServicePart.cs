using MediatR;

namespace Application.UseCase.OrderServiceParts;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateOrderServicePart.
public sealed record CreateOrderServicePart(int OrderServiceId, int PartId, int Quantity, decimal AppliedUnitPrice, bool? CustomerApproved = null) : IRequest<int>;
