namespace Api.DTOs.VehicleBrands;

// DTO usado para transportar datos de UpdateVehicleBrandRequest entre la API y sus consumidores.
public sealed record UpdateVehicleBrandRequest(string BrandName);
