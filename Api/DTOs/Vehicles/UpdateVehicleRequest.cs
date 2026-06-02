namespace Api.DTOs.Vehicles;

public sealed record UpdateVehicleRequest(
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true);
