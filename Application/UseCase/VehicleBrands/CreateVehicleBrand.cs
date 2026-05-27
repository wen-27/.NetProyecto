using MediatR;

namespace Application.UseCase.VehicleBrands;

public sealed record CreateVehicleBrand(string BrandName) : IRequest<int>;
