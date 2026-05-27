using Domain.Common;

namespace Domain.Entities;

public class Address : BaseEntity
{
    public int NeighborhoodId { get; set; }
    public int StreetTypeId { get; set; }
    public string? MainNumber { get; set; }
    public string? SecondaryNumber { get; set; }
    public string? TertiaryNumber { get; set; }
    public string? Complement { get; set; }

    public Neighborhood Neighborhood { get; set; } = null!;
    public StreetType StreetType { get; set; } = null!;
}
