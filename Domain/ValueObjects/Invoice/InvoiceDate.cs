// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceDate, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
namespace Domain.ValueObjects.Invoice;

public readonly record struct InvoiceDate
{
    public DateTime Value { get; }

    public InvoiceDate(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de la factura es obligatoria.", nameof(value));
        }

        if (value > DateTime.UtcNow.AddMinutes(5))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La fecha de la factura no puede estar en el futuro.");
        }

        Value = value;
    }

    public override string ToString() => Value.ToString("O");
}
