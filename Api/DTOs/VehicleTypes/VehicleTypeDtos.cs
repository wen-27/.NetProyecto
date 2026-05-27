namespace Api.DTOs.VehicleTypes;

public sealed record CreateVehicleTypeRequest(string Name);
public sealed record UpdateVehicleTypeRequest(string Name);
public sealed record VehicleTypeResponse(int Id, string Name);
