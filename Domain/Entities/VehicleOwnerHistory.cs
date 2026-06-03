using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa VehicleOwnerHistory dentro del modelo principal del taller.
public class VehicleOwnerHistory : BaseEntity 
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int VehicleId { get; set; }
    public int PersonId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
    public Person Person { get; set; } = null!;
}
