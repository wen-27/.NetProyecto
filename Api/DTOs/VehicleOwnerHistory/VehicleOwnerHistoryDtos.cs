namespace Api.DTOs.VehicleOwnerHistory;

// DTO usado para transportar datos de CreateVehicleOwnerHistoryRequest entre la API y sus consumidores.
public sealed record CreateVehicleOwnerHistoryRequest(int VehicleId, int PersonId, DateOnly StartDate);
// DTO usado para transportar datos de RegisterVehicleOwnerRequest entre la API y sus consumidores.
public sealed record RegisterVehicleOwnerRequest(int VehicleId, int PersonId, DateOnly StartDate);
// DTO usado para transportar datos de UpdateVehicleOwnerHistoryRequest entre la API y sus consumidores.
public sealed record UpdateVehicleOwnerHistoryRequest(int VehicleId, int PersonId, DateOnly StartDate, DateOnly? EndDate);
// DTO usado para transportar datos de VehicleOwnerHistoryResponse entre la API y sus consumidores.
public sealed record VehicleOwnerHistoryResponse(int Id, int VehicleId, int PersonId, DateOnly StartDate, DateOnly? EndDate);
