namespace Api.DTOs.VehicleBrands;

public sealed record CreateVehicleBrandRequest(string BrandName);
public sealed record VehicleBrandResponse(int Id, string BrandName);
