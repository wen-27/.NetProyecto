using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa VehicleBrand dentro del modelo principal del taller.
public class VehicleBrand : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string BrandName { get; set; } = string.Empty;

    public ICollection<VehicleModel> Models { get; set; } = new List<VehicleModel>();
}
