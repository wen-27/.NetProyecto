using Domain.ValueObjects;

namespace Domain.ValueObjects.OrderPartDetail;

public readonly record struct OrderPartDetailAppliedUnitPrice
{
    public decimal Amount { get; }
    public decimal Value => Amount;

    public OrderPartDetailAppliedUnitPrice(decimal amount)
    {
        Amount = ValueObjectValidation.Money(amount, nameof(OrderPartDetailAppliedUnitPrice));
    }

    public static OrderPartDetailAppliedUnitPrice Zero() => new(0);

    public OrderPartDetailAppliedUnitPrice Add(OrderPartDetailAppliedUnitPrice other) => new(Amount + other.Amount);

    public OrderPartDetailAppliedUnitPrice Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad no puede ser negativa.");
        }

        return new OrderPartDetailAppliedUnitPrice(Amount * quantity);
    }

    public override string ToString() => Amount.ToString("F2");
}
