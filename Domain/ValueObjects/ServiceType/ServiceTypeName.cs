// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de ServiceTypeName, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

public readonly record struct ServiceTypeName
{
    public ServiceTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(ServiceTypeName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
