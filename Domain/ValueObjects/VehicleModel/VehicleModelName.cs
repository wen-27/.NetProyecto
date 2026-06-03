// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleModelName, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleModel;

public readonly record struct VehicleModelName
{
    public VehicleModelName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(VehicleModelName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
