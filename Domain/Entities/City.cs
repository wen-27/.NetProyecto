using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa City dentro del modelo principal del taller.
public class City : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;

    public Department Department { get; set; } = null!;
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}
