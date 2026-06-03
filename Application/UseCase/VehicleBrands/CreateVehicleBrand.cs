using MediatR;

namespace Application.UseCase.VehicleBrands;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateVehicleBrand.
public sealed record CreateVehicleBrand(string BrandName) : IRequest<int>;
