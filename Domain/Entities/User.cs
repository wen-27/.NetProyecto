// Responsabilidad: Entidad de dominio User; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
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
