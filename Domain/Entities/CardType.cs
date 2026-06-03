// Responsabilidad: Entidad de dominio CardType; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class CardType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();
}
