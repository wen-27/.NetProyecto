// Responsabilidad: Entidad de dominio Payment; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    public int InvoiceId { get; set; }
    public int PaymentMethodId { get; set; }
    public int PaymentStatusId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string? Reference { get; set; }
    public int? ClientPersonId { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public int? VerifiedByReceptionistPersonId { get; set; }
    public string? RejectedReason { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public Invoice Invoice { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public PaymentStatus PaymentStatus { get; set; } = null!;
    public Person? ClientPerson { get; set; }
    public Person? VerifiedByReceptionistPerson { get; set; }
    public PaymentCard? PaymentCard { get; set; }
}
