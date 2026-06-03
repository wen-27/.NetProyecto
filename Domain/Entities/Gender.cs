using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Gender dentro del modelo principal del taller.
public class Gender : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;
}
