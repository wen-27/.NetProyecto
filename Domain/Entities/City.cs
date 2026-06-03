// Responsabilidad: Entidad de dominio City; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class City : BaseEntity
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;

    public Department Department { get; set; } = null!;
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}
