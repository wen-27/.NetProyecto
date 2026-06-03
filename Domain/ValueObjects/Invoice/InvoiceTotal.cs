// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceTotal, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

public readonly record struct InvoiceTotal
{
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
