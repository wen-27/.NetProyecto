using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa StreetType dentro del modelo principal del taller.
public class StreetType : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;
}
