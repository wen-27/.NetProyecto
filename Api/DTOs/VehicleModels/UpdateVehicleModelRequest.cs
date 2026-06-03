namespace Api.DTOs.VehicleModels;

// DTO usado para transportar datos de UpdateVehicleModelRequest entre la API y sus consumidores.
public sealed record UpdateVehicleModelRequest(int BrandId, string ModelName);
