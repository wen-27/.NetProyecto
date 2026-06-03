// Responsabilidad: Entidad de dominio MechanicSpecialtyAssignment; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class MechanicSpecialtyAssignment : BaseEntity
{
    public int PersonId { get; set; }
    public int SpecialtyId { get; set; }

    public Person Person { get; set; } = null!;
    public MechanicSpecialty Specialty { get; set; } = null!;
}
