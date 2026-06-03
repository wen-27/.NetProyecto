namespace Api.DTOs.VehicleBrands;

// DTO usado para transportar datos de CreateVehicleBrandRequest entre la API y sus consumidores.
public sealed record CreateVehicleBrandRequest(string BrandName);
// DTO usado para transportar datos de VehicleBrandResponse entre la API y sus consumidores.
public sealed record VehicleBrandResponse(int Id, string BrandName);
