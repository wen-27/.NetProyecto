using MediatR;

namespace Application.UseCase.Vehicles;

// Caso de uso que modela una accion o consulta de negocio relacionada con CreateVehicle.
public sealed record CreateVehicle(
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true) : IRequest<int>;
