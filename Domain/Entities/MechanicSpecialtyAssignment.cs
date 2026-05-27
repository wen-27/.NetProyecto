using Domain.Common;

namespace Domain.Entities;

public class MechanicSpecialtyAssignment : BaseEntity
{
    public int PersonId { get; set; }
    public int SpecialtyId { get; set; }

    public Person Person { get; set; } = null!;
    public MechanicSpecialty Specialty { get; set; } = null!;
}
