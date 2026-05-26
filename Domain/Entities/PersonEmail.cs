using Domain.Common;

namespace Domain.Entities;

public class PersonEmail : BaseEntity
{
    public int PersonId { get; set; }
    public int? EmailDomainId { get; set; }
    public string Email { get; set; } = string.Empty;
    public Person Person { get; set; } = null!;
    public EmailDomain? EmailDomain { get; set; }
}
