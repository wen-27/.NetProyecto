// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateServiceType. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed record UpdateServiceType(int Id, string Name, int EstimatedDays = 1) : IRequest;
