using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Vehicle dentro del modelo principal del taller.
public class Vehicle : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int ModelId { get; set; }
    public int VehicleTypeId { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Color { get; set; }
    public int Mileage { get; set; }

    public VehicleModel VehicleModel { get; set; } = null!;
    public VehicleType VehicleType { get; set; } = null!;
    public ICollection<VehicleOwnerHistory> OwnerHistory { get; set; } = new List<VehicleOwnerHistory>();
    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}
