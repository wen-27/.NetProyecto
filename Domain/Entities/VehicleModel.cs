using Domain.Common;

namespace Domain.Entities;

public class VehicleModel : BaseEntity
{
    public int VehicleBrandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }

    public VehicleBrand VehicleBrand { get; set; } = null!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
