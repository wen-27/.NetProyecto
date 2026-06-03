// Responsabilidad: Entidad de dominio VehicleBrand; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class VehicleBrand : BaseEntity
{
    public string BrandName { get; set; } = string.Empty;

    public ICollection<VehicleModel> Models { get; set; } = new List<VehicleModel>();
}
