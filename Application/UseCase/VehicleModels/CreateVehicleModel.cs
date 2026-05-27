using MediatR;

namespace Application.UseCase.VehicleModels;

public sealed record CreateVehicleModel(int BrandId, string ModelName) : IRequest<int>;
