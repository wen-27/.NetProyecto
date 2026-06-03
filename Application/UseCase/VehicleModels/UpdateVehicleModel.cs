using MediatR;

namespace Application.UseCase.VehicleModels;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateVehicleModel.
public sealed record UpdateVehicleModel(int Id, int BrandId, string ModelName) : IRequest;
