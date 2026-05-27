using MediatR;

namespace Application.UseCase.ServiceOrderServices;

public sealed record RemoveServiceFromOrder(int Id) : IRequest;
