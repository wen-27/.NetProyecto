using MediatR;

namespace Application.UseCase.VehicleBrands;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateVehicleBrand.
public sealed record UpdateVehicleBrand(int Id, string BrandName) : IRequest;
