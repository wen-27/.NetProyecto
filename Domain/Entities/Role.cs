using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa Role dentro del modelo principal del taller.
public class Role : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public string RoleName { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<PersonRole> PersonRoles { get; set; } = new List<PersonRole>();
}
