// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateVehicle. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Vehicles;

public sealed record CreateVehicle(
    int ModelId,
    int VehicleTypeId,
    string Plate,
    string Vin,
    int Year,
    string? Color,
    int Mileage,
    bool IsActive = true) : IRequest<int>;
