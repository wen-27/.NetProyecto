// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PaymentStatusValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentStatus;

public readonly record struct PaymentStatusName
{
    public PaymentStatusName(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentStatusName), 50);
    public string Value { get; }
}
