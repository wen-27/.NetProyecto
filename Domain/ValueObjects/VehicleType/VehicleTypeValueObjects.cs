// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleTypeValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleType;

public readonly record struct VehicleTypeName
{
    public VehicleTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(VehicleTypeName), 80);
    public string Value { get; }
}
