using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

// Caso de uso que modela una accion o consulta de negocio relacionada con EndVehicleOwnership.
public sealed record EndVehicleOwnership(int VehicleId, DateTime EndDate) : IRequest;
