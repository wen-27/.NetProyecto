// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RegisterVehicleOwner. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.VehicleOwnerHistory;

public sealed record RegisterVehicleOwner(int VehicleId, int PersonId, DateTime StartDate) : IRequest<int>;
