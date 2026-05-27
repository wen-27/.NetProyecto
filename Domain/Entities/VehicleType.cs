using Domain.Common;

namespace Domain.Entities;

public class VehicleType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
