namespace Api.DTOs.VehicleOwnerHistory;

public sealed record CreateVehicleOwnerHistoryRequest(int VehicleId, int PersonId, DateOnly StartDate);
public sealed record RegisterVehicleOwnerRequest(int VehicleId, int PersonId, DateOnly StartDate);
public sealed record UpdateVehicleOwnerHistoryRequest(int VehicleId, int PersonId, DateOnly StartDate, DateOnly? EndDate);
public sealed record VehicleOwnerHistoryResponse(int Id, int VehicleId, int PersonId, DateOnly StartDate, DateOnly? EndDate);
