using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed record UpdateServiceOrderPart(int Id, int Quantity, decimal AppliedUnitPrice) : IRequest;
