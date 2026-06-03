// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con DeleteVehicle. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Vehicles;

public sealed record DeleteVehicle(int Id) : IRequest;
