// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateServiceType. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.ServiceTypes;

public sealed record CreateServiceType(string Name, int EstimatedDays = 1) : IRequest<int>;
