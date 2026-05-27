using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed record CreateServiceType(string Name, int EstimatedDays = 1) : IRequest<int>;
