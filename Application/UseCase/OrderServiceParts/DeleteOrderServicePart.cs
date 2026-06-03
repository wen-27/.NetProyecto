// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con DeleteOrderServicePart. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed record DeleteOrderServicePart(int Id) : IRequest;
