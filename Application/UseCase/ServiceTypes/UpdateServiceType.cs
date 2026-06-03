using MediatR;

namespace Application.UseCase.ServiceTypes;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateServiceType.
public sealed record UpdateServiceType(int Id, string Name, int EstimatedDays = 1) : IRequest;
