using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public int PersonId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool Status { get; set; } = true;

    public Person Person { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<ServiceOrderService> ServiceOrderServicesAsMechanic { get; set; } = new List<ServiceOrderService>();
    public ICollection<OrderStatusHistory> OrderStatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
