namespace Api.DTOs.Vehicles;

// DTO usado para transportar datos de UpdateVehicleRequest entre la API y sus consumidores.
public sealed record UpdateVehicleRequest(
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true);
