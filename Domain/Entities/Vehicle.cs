using Domain.Common;

namespace Domain.Entities;

public class Vehicle : BaseEntity
{
    public int VehicleModelId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string? Vin { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }

    public VehicleModel VehicleModel { get; set; } = null!;
    public ICollection<VehicleOwnerHistory> OwnerHistory { get; set; } = new List<VehicleOwnerHistory>();
    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}