using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

// Value Object que encapsula y valida un valor especifico de InvoiceTotal.
public readonly record struct InvoiceTotal
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public decimal Amount { get; }
    public decimal Value => Amount;

    public InvoiceTotal(decimal amount)
    {
        Amount = ValueObjectValidation.Money(amount, nameof(InvoiceTotal));
    }

    public static InvoiceTotal Zero() => new(0);

    public InvoiceTotal Add(InvoiceTotal other) => new(Amount + other.Amount);

    public InvoiceTotal Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad no puede ser negativa.");
        }

        return new InvoiceTotal(Amount * quantity);
    }

    public override string ToString() => Amount.ToString("F2");
}
