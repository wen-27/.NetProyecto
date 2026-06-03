using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PersonPhone dentro del modelo principal del taller.
public class PersonPhone : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PersonId { get; set; }
    public int CountryId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Person Person { get; set; } = null!;
    public Country Country { get; set; } = null!;
}
