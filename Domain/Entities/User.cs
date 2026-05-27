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
    public ICollection<ServiceOrderService> ServiceOrderServicesAsMechanic { get; set; } = new List<ServiceOrderService>();
    public ICollection<OrderStatusHistory> OrderStatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
