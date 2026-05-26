using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string PersonId { get; set; } 
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime? LastLoginAt { get; set; }

    public Person Person { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<ServiceOrder> OrdersAsMechanic { get; set; } = new List<ServiceOrder>();

}