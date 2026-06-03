using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa DocumentType dentro del modelo principal del taller.
public class DocumentType : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<Person> Persons { get; set; } = new List<Person>();
}
