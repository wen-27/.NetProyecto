namespace Api.DTOs.VehicleModels;

// DTO usado para transportar datos de CreateVehicleModelRequest entre la API y sus consumidores.
public sealed record CreateVehicleModelRequest(int BrandId, string ModelName);
// DTO usado para transportar datos de VehicleModelResponse entre la API y sus consumidores.
public sealed record VehicleModelResponse(int Id, int BrandId, string ModelName);
