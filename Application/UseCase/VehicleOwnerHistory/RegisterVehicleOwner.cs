using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed record RegisterVehicleOwner(int VehicleId, int PersonId, DateTime StartDate) : IRequest<int>;
