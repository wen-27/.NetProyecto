using Domain.Common;

namespace Domain.Entities;

public class CardType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();
}
