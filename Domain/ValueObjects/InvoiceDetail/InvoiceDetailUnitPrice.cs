// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceDetailUnitPrice, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

public readonly record struct InvoiceDetailUnitPrice
{
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
