using Domain.Common;

namespace Domain.Entities;

public class AuditActionType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Audit> Audits { get; set; } = new List<Audit>();
}