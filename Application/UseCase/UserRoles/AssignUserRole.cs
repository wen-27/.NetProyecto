// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con AssignUserRole. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.UserRoles;

public sealed record AssignUserRole(int UserId, int RoleId) : IRequest;
