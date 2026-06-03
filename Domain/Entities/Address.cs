using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Address dentro del modelo principal del taller.
public class Address : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int NeighborhoodId { get; set; }
    public int StreetTypeId { get; set; }
    public string? MainNumber { get; set; }
    public string? SecondaryNumber { get; set; }
    public string? TertiaryNumber { get; set; }
    public string? Complement { get; set; }

    public Neighborhood Neighborhood { get; set; } = null!;
    public StreetType StreetType { get; set; } = null!;
}
