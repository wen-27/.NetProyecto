// Responsabilidad: Entidad de dominio ServiceType; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class ServiceType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int EstimatedDays { get; set; } = 1;

    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
}
