using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Payment dentro del modelo principal del taller.
public class Payment : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
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
