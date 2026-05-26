using Domain.Common;

namespace Domain.Entities;

public class VehicleModel : BaseEntity
{
    public int BrandId { get; set; }
    public string ModelName { get; set; } = string.Empty;

    public VehicleBrand VehicleBrand { get; set; } = null!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
