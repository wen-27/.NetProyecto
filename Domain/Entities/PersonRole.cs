using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PersonRole dentro del modelo principal del taller.
public class PersonRole : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PersonId { get; set; }
    public int RoleId { get; set; }

    public Person Person { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
