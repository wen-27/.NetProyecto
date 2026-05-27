using Domain.Common;

namespace Domain.Entities;

public class PaymentStatus : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
