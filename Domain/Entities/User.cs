using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa User dentro del modelo principal del taller.
public class User : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PersonId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }

    public bool Status
    {
        get => IsActive;
        set => IsActive = value;
    }

    public Person Person { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<OrderStatusHistory> OrderStatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
