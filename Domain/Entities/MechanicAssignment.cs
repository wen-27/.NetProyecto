using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa MechanicAssignment dentro del modelo principal del taller.
public class MechanicAssignment : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int OrderServiceId { get; set; }
    public int MechanicPersonId { get; set; }
    public int SpecialtyId { get; set; }

    public OrderService OrderService { get; set; } = null!;
    public Person MechanicPerson { get; set; } = null!;
    public MechanicSpecialty Specialty { get; set; } = null!;
}
