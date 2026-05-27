using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed record CreateServiceType(string Name) : IRequest<int>;
