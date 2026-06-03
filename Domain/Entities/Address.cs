// Responsabilidad: Entidad de dominio Address; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Address : BaseEntity
{
    public int NeighborhoodId { get; set; }
    public int StreetTypeId { get; set; }
    public string? MainNumber { get; set; }
    public string? SecondaryNumber { get; set; }
    public string? TertiaryNumber { get; set; }
    public string? Complement { get; set; }

    public Neighborhood Neighborhood { get; set; } = null!;
    public StreetType StreetType { get; set; } = null!;
}
