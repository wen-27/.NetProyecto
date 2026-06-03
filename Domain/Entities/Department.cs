using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Department dentro del modelo principal del taller.
public class Department : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int CountryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public Country Country { get; set; } = null!;
    public ICollection<City> Cities { get; set; } = new List<City>();
}
