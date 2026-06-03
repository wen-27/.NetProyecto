// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de InvoiceStatusValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.InvoiceStatus;

public readonly record struct InvoiceStatusName
{
    public InvoiceStatusName(string value) => Value = ValueObjectValidation.Required(value, nameof(InvoiceStatusName), 50);
    public string Value { get; }
}
