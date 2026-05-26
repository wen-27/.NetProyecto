using Domain.Common;

namespace Domain.Entities;

public class EmailDomain : BaseEntity
{
    public string Domain { get; set; } = string.Empty;
    public ICollection<PersonEmail> PersonEmails { get; set; } = new List<PersonEmail>();
}
