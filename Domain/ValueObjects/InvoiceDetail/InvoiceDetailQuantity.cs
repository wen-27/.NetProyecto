// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceDetailQuantity, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

public readonly record struct InvoiceDetailQuantity
{
    public InvoiceDetailQuantity(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceDetailQuantity));
    }
    public int Value { get; }
}
