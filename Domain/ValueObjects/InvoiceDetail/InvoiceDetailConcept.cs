// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceDetailConcept, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceDetail;

public readonly record struct InvoiceDetailConcept
{
    public InvoiceDetailConcept(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(InvoiceDetailConcept), 150);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
