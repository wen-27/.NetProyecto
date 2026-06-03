namespace Api.DTOs.VehicleOwnerHistory;

// DTO usado para transportar datos de EndVehicleOwnershipRequest entre la API y sus consumidores.
public sealed record EndVehicleOwnershipRequest(DateTime EndDate);
