using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PaymentMethod dentro del modelo principal del taller.
public class PaymentMethod : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
