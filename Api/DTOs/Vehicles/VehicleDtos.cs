namespace Api.DTOs.Vehicles;

public sealed record CreateVehicleRequest(
    int ModelId,
    int VehicleTypeId,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true);

public sealed record VehicleResponse(
    int Id,
    int ModelId,
    int VehicleTypeId,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive);
