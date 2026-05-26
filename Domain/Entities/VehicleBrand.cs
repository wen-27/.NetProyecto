using Domain.Common;

namespace Domain.Entities;

public class VehicleBrand : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<VehicleModel> Models { get; set; } = new List<VehicleModel>();

}