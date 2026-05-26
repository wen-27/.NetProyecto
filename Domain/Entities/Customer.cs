using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    public int PersonId { get; set; }
    public bool Status { get; set; } = true;

    public Person Person { get; set; } = null!;
    public ICollection<VehicleOwnerHistory> VehicleHistory { get; set; } = new List<VehicleOwnerHistory>();
}
