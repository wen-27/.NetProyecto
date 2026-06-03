using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa UserRole dentro del modelo principal del taller.
public class UserRole
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
