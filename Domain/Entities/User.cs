using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public int PersonId { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool Status { get; set; } = true;

    public Person Person { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<ServiceOrder> OrdersAsMechanic { get; set; } = new List<ServiceOrder>();
    public ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
