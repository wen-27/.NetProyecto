// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de StreetTypeValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.StreetType;

public readonly record struct StreetTypeName
{
    public StreetTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(StreetTypeName), 50);
    public string Value { get; }
}
