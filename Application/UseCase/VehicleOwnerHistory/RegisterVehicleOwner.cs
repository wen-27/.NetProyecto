using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con RegisterVehicleOwner.
public sealed record RegisterVehicleOwner(int VehicleId, int PersonId, DateTime StartDate) : IRequest<int>;
