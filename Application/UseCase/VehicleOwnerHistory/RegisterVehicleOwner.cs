using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed record RegisterVehicleOwner(int VehicleId, int CustomerId, DateTime StartDate) : IRequest<int>;
