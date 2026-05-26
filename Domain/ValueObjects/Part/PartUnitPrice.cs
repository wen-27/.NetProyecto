using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartUnitPrice
{
    public decimal Amount { get; }
    public decimal Value => Amount;

    public PartUnitPrice(decimal amount)
    {
        Amount = ValueObjectValidation.Money(amount, nameof(PartUnitPrice));
    }

    public static PartUnitPrice Zero() => new(0);

    public PartUnitPrice Add(PartUnitPrice other) => new(Amount + other.Amount);

    public PartUnitPrice Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad no puede ser negativa.");
        }

        return new PartUnitPrice(Amount * quantity);
    }

    public override string ToString() => Amount.ToString("F2");
}
