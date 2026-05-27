using Domain.Common;

namespace Domain.Entities;

public class PersonRole : BaseEntity
{
    public int PersonId { get; set; }
    public int RoleId { get; set; }

    public Person Person { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
