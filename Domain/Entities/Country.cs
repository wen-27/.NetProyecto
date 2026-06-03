using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Country dentro del modelo principal del taller.
public class Country : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;
    public string? PhoneCode { get; set; }
}
