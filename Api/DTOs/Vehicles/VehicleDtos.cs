namespace Api.DTOs.Vehicles;

// DTO usado para transportar datos de CreateVehicleRequest entre la API y sus consumidores.
public sealed record CreateVehicleRequest(
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true);

// DTO usado para transportar datos de VehicleResponse entre la API y sus consumidores.
public sealed record VehicleResponse(
    int Id,
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive);
