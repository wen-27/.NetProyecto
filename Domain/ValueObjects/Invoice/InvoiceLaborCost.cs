using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

public readonly record struct InvoiceLaborCost
{
    public decimal Amount { get; }
    public decimal Value => Amount;

    public InvoiceLaborCost(decimal amount)
    {
        Amount = ValueObjectValidation.Money(amount, nameof(InvoiceLaborCost));
    }

    public static InvoiceLaborCost Zero() => new(0);

    public InvoiceLaborCost Add(InvoiceLaborCost other) => new(Amount + other.Amount);

    public InvoiceLaborCost Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad no puede ser negativa.");
        }

        return new InvoiceLaborCost(Amount * quantity);
    }

    public override string ToString() => Amount.ToString("F2");
}
