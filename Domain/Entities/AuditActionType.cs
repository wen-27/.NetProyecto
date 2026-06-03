using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa AuditActionType dentro del modelo principal del taller.
public class AuditActionType : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<Audit> Audits { get; set; } = new List<Audit>();
}