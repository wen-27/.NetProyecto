// Responsabilidad: Entidad de dominio Department; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Department : BaseEntity
{
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public Country Country { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = new List<City>();
}
