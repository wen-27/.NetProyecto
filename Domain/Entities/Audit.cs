using Domain.Common;

namespace Domain.Entities;

public class Audit : BaseEntity
{
    public int AuditActionTypeId { get; set; }
    public int? UserId { get; set; }

    public string Entity { get; set; } = string.Empty;
    public int? EntityId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? IpAddress { get; set; }

    public AuditActionType AuditActionType { get; set; } = null!;
    public User? User { get; set; }
}