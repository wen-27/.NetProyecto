using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    public int PersonId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    public Person Person { get; set; } = null!;
    public ICollection<VehicleOwnerHistory> VehicleHistory { get; set; } = new List<VehicleOwnerHistory>();
}
