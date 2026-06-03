// Responsabilidad: Entidad de dominio PersonRole; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PersonRole : BaseEntity
{
    public int PersonId { get; set; }
    public int RoleId { get; set; }

    public Person Person { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
