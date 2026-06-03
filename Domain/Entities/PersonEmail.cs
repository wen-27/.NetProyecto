// Responsabilidad: Entidad de dominio PersonEmail; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PersonEmail : BaseEntity
{
    public int PersonId { get; set; }
    public int EmailDomainId { get; set; }
    public string EmailUser { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Person Person { get; set; } = null!;
    public EmailDomain EmailDomain { get; set; } = null!;
}
