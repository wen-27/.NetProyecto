using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

// Value Object que encapsula y valida un valor especifico de InvoiceDetailUnitPrice.
public readonly record struct InvoiceDetailUnitPrice
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public decimal Amount { get; }
    public decimal Value => Amount;

    public InvoiceDetailUnitPrice(decimal amount)
    {
        Amount = ValueObjectValidation.Money(amount, nameof(InvoiceDetailUnitPrice));
    }

    public static InvoiceDetailUnitPrice Zero() => new(0);

    public InvoiceDetailUnitPrice Add(InvoiceDetailUnitPrice other) => new(Amount + other.Amount);

    public InvoiceDetailUnitPrice Multiply(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad no puede ser negativa.");
        }

        return new InvoiceDetailUnitPrice(Amount * quantity);
    }

    public override string ToString() => Amount.ToString("F2");
}
