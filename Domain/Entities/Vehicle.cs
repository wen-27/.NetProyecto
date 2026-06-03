// Responsabilidad: Entidad de dominio Vehicle; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class Vehicle : BaseEntity
{
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
