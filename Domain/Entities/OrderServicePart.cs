using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa OrderServicePart dentro del modelo principal del taller.
public class OrderServicePart : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int OrderServiceId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal AppliedUnitPrice { get; set; }
    public bool? CustomerApproved { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public OrderService OrderService { get; set; } = null!;
    public Part Part { get; set; } = null!;
}
