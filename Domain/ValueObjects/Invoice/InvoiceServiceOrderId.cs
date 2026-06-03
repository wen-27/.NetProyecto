// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceServiceOrderId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Invoice;

public readonly record struct InvoiceServiceOrderId
{
    public InvoiceServiceOrderId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(InvoiceServiceOrderId));
    }
    public int Value { get; }
}
