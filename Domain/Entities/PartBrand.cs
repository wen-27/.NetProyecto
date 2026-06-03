using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PartBrand dentro del modelo principal del taller.
public class PartBrand : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Name { get; set; } = string.Empty;

    public ICollection<Part> Parts { get; set; } = new List<Part>();
}
