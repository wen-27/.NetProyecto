using MediatR;

namespace Application.UseCase.OrderPartDetails;

public sealed record RemovePartFromServiceOrder(int Id) : IRequest;
