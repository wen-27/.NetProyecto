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
