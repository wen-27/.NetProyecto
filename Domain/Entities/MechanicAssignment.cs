// Responsabilidad: Entidad de dominio MechanicAssignment; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class MechanicAssignment : BaseEntity
{
    public int OrderServiceId { get; set; }
    public int MechanicPersonId { get; set; }
    public int SpecialtyId { get; set; }

    public OrderService OrderService { get; set; } = null!;
    public Person MechanicPerson { get; set; } = null!;
    public MechanicSpecialty Specialty { get; set; } = null!;
}
