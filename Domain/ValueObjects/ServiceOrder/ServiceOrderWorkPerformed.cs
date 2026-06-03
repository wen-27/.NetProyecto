// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ServiceOrderWorkPerformed, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderWorkPerformed
{
    public string? Value { get; }

    public ServiceOrderWorkPerformed(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderWorkPerformed), 5000);
    }

    public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

    public override string ToString() => Value ?? string.Empty;
}
