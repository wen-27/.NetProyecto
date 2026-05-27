using Domain.Common;

namespace Domain.Entities;

public class PartBrand : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Part> Parts { get; set; } = new List<Part>();
}
