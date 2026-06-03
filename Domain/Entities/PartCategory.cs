// Responsabilidad: Entidad de dominio PartCategory; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PartCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Part> Parts { get; set; } = new List<Part>();
}