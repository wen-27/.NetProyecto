// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdateOrderServicePart. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.OrderServiceParts;

public sealed record UpdateOrderServicePart(int Id, int Quantity, decimal AppliedUnitPrice, bool? CustomerApproved = null) : IRequest;
