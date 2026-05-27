using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed record EndVehicleOwnership(int VehicleId, DateTime EndDate) : IRequest;
