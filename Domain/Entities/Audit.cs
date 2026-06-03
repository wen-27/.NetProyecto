using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Audit dentro del modelo principal del taller.
public class Audit : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int UserId { get; set; }
    public int AuditActionTypeId { get; set; }
    public string AffectedEntity { get; set; } = string.Empty;
    public int AffectedRecordId { get; set; }
    public string? Description { get; set; }

    public User User { get; set; } = null!;
    public AuditActionType AuditActionType { get; set; } = null!;
}
