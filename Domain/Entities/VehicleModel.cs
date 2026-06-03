using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa VehicleModel dentro del modelo principal del taller.
public class VehicleModel : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int BrandId { get; set; }
    public string ModelName { get; set; } = string.Empty;

    public VehicleBrand VehicleBrand { get; set; } = null!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
