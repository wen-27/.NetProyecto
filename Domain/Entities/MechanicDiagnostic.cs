// Responsabilidad: Entidad de dominio MechanicDiagnostic; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class MechanicDiagnostic : BaseEntity
{
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
