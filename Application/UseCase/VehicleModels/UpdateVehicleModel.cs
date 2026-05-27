using MediatR;

namespace Application.UseCase.VehicleModels;

public sealed record UpdateVehicleModel(int Id, int BrandId, string ModelName) : IRequest;
