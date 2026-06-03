// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con CreateMechanicAssignment. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.MechanicAssignments;

public sealed record CreateMechanicAssignment(int OrderServiceId, int MechanicPersonId, int SpecialtyId) : IRequest<int>;
