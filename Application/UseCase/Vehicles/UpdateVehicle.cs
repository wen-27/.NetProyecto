using MediatR;

namespace Application.UseCase.Vehicles;

// Caso de uso que modela una accion o consulta de negocio relacionada con UpdateVehicle.
public sealed record UpdateVehicle(
    int Id,
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true) : IRequest;
