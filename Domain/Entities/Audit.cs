// Responsabilidad: Entidad de dominio Audit; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Audit : BaseEntity
{
    public int UserId { get; set; }
    public int AuditActionTypeId { get; set; }
    public string AffectedEntity { get; set; } = string.Empty;
    public int AffectedRecordId { get; set; }
    public string? Description { get; set; }

    public User User { get; set; } = null!;
    public AuditActionType AuditActionType { get; set; } = null!;
}
