using Domain.Common;

namespace Domain.Entities;

public class City : BaseEntity
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;

    public Department Department { get; set; } = null!;
    public ICollection<Neighborhood> Neighborhoods { get; set; } = new List<Neighborhood>();
}
