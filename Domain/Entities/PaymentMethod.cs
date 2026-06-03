// Responsabilidad: Entidad de dominio PaymentMethod; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
