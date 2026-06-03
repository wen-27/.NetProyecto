// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con RegisterAudit. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using MediatR;

namespace Application.UseCase.Audits;

public sealed record RegisterAudit(
    int UserId,
    int AuditActionTypeId,
    string AffectedEntity,
    int AffectedRecordId,
    string? Description) : IRequest<int>;
