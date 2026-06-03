using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

// Entidad de dominio que representa MechanicDiagnostic dentro del modelo principal del taller.
public class MechanicDiagnostic : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int ServiceOrderId { get; set; }
    public int MechanicPersonId { get; set; }
    public int? WorkshopChiefPersonId { get; set; }
    public MechanicDiagnosticStatus Status { get; set; } = MechanicDiagnosticStatus.PendingWorkshopChiefApproval;
    public string Findings { get; set; } = string.Empty;
    public string RecommendedWork { get; set; } = string.Empty;
    public string? WorkshopChiefComment { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }

    public ServiceOrder ServiceOrder { get; set; } = null!;
    public Person MechanicPerson { get; set; } = null!;
    public Person? WorkshopChiefPerson { get; set; }
}
