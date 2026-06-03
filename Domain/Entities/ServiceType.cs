using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa ServiceType dentro del modelo principal del taller.
public class ServiceType : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;
    public int EstimatedDays { get; set; } = 1;

    public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
}
