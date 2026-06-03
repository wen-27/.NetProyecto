// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con ChangeUserStatus. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Users;

public sealed record ChangeUserStatus(int Id, bool Status) : IRequest;
