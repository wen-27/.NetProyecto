using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed record UpdateServiceType(int Id, string Name, int EstimatedDays = 1) : IRequest;
