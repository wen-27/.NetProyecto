using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed record CreateOrderServicePart(int OrderServiceId, int PartId, int Quantity, decimal AppliedUnitPrice, bool? CustomerApproved = null) : IRequest<int>;
