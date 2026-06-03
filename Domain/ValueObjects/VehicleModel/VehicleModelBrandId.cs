// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de VehicleModelBrandId, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleModel;

public readonly record struct VehicleModelBrandId
{
    public VehicleModelBrandId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleModelBrandId));
    }
    public int Value { get; }
}
