using Domain.Common;

namespace Domain.Entities;

public class PersonAddress : BaseEntity
{
    public int PersonId { get; set; }
    public int CityId { get; set; }
    public string Address { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Person Person { get; set; } = null!;
    public City City { get; set; } = null!;
}
