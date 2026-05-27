using MediatR;

namespace Application.UseCase.Vehicles;

public sealed record CreateVehicle(int ModelId, int VehicleTypeId, string Vin, int Year, int Mileage) : IRequest<int>;
