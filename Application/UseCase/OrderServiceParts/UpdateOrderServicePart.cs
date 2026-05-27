using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed record UpdateOrderServicePart(int Id, int Quantity, decimal AppliedUnitPrice, bool? CustomerApproved = null) : IRequest;
