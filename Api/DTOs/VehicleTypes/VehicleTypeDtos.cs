namespace Api.DTOs.VehicleTypes;

// DTO usado para transportar datos de CreateVehicleTypeRequest entre la API y sus consumidores.
public sealed record CreateVehicleTypeRequest(string Name);
// DTO usado para transportar datos de UpdateVehicleTypeRequest entre la API y sus consumidores.
public sealed record UpdateVehicleTypeRequest(string Name);
// DTO usado para transportar datos de VehicleTypeResponse entre la API y sus consumidores.
public sealed record VehicleTypeResponse(int Id, string Name);
