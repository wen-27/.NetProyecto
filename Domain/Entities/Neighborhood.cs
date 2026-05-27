using Domain.Common;

namespace Domain.Entities;

public class Neighborhood : BaseEntity
{
    public int CityId { get; set; }
    public string Name { get; set; } = string.Empty;

    public City City { get; set; } = null!;
}
