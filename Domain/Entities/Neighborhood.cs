// Responsabilidad: Entidad de dominio Neighborhood; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Neighborhood : BaseEntity
{
    public int CityId { get; set; }
    public string Name { get; set; } = string.Empty;

    public City City { get; set; } = null!;
}
