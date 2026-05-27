using MediatR;

namespace Application.UseCase.VehicleBrands;

public sealed record UpdateVehicleBrand(int Id, string BrandName) : IRequest;
