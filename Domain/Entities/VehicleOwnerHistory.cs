// Responsabilidad: Entidad de dominio VehicleOwnerHistory; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class VehicleOwnerHistory : BaseEntity 
{
    public int VehicleId { get; set; }
    public int PersonId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
    public Person Person { get; set; } = null!;
}
