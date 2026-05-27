using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed record DeleteOrderServicePart(int Id) : IRequest;
