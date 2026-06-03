using MediatR;

namespace Application.UseCase.VehicleModels;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateVehicleModel.
public sealed record CreateVehicleModel(int BrandId, string ModelName) : IRequest<int>;
