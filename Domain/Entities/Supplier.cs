using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Supplier dentro del modelo principal del taller.
public class Supplier : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;
    public string? TaxId { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool Status { get; set; } = true;

    public ICollection<PartPurchase> PartPurchases { get; set; } = new List<PartPurchase>();
}
