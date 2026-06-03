// Responsabilidad: Entidad de dominio EmailDomain; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class EmailDomain : BaseEntity
{
    public string Domain { get; set; } = string.Empty;
    public ICollection<PersonEmail> PersonEmails { get; set; } = new List<PersonEmail>();
}
