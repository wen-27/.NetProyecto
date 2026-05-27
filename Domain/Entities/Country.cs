using Domain.Common;

namespace Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? PhoneCode { get; set; }
}
