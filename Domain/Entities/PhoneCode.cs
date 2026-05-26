using Domain.Common;

namespace Domain.Entities;

public class PhoneCode : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public ICollection<PersonPhone> PersonPhones { get; set; } = new List<PersonPhone>();
}
