using Domain.Common;

namespace Domain.Entities;

public class PersonPhone : BaseEntity
{
    public int PersonId { get; set; }
    public int PhoneCodeId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Person Person { get; set; } = null!;
    public PhoneCode PhoneCode { get; set; } = null!;
}
