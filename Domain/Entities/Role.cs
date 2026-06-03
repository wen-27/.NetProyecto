// Responsabilidad: Entidad de dominio Role; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string RoleName { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<PersonRole> PersonRoles { get; set; } = new List<PersonRole>();
}
