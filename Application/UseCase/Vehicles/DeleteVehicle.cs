using MediatR;

namespace Application.UseCase.Vehicles;

// Caso de uso que modela una accion o consulta de negocio relacionada con DeleteVehicle.
public sealed record DeleteVehicle(int Id) : IRequest;
