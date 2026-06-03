// Responsabilidad: Entidad de dominio PersonPhone; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PersonPhone : BaseEntity
{
    public int PersonId { get; set; }
    public int CountryId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Person Person { get; set; } = null!;
    public Country Country { get; set; } = null!;
}
