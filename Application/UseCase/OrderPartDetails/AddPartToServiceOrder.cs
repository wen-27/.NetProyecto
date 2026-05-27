using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed record AddPartToServiceOrder(int ServiceOrderId, int PartId, int Quantity, decimal AppliedUnitPrice) : IRequest<int>;
