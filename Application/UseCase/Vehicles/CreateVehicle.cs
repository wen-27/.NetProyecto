using MediatR;

namespace Application.UseCase.Vehicles;

public sealed record CreateVehicle(
    int ModelId,
    int VehicleTypeId,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true) : IRequest<int>;
