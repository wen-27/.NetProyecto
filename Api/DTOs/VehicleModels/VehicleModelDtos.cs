namespace Api.DTOs.VehicleModels;

public sealed record CreateVehicleModelRequest(int BrandId, string ModelName);
public sealed record VehicleModelResponse(int Id, int BrandId, string ModelName);
