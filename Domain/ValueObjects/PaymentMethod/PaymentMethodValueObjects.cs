// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PaymentMethodValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.PaymentMethod;

public readonly record struct PaymentMethodName
{
    public PaymentMethodName(string value) => Value = ValueObjectValidation.Required(value, nameof(PaymentMethodName), 50);
    public string Value { get; }
}
