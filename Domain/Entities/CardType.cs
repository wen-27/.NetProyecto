using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa CardType dentro del modelo principal del taller.
public class CardType : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();
}
