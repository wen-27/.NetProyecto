using MediatR;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateServiceType.
public sealed record CreateServiceType(string Name, int EstimatedDays = 1) : IRequest<int>;
