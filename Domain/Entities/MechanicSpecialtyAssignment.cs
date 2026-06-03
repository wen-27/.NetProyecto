using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa MechanicSpecialtyAssignment dentro del modelo principal del taller.
public class MechanicSpecialtyAssignment : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PersonId { get; set; }
    public int SpecialtyId { get; set; }

    public Person Person { get; set; } = null!;
    public MechanicSpecialty Specialty { get; set; } = null!;
}
