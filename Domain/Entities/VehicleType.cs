using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa VehicleType dentro del modelo principal del taller.
public class VehicleType : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
