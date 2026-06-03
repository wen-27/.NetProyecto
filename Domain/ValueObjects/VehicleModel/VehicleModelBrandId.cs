using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleModel;

// Value Object que encapsula y valida un valor especifico de VehicleModelBrandId.
public readonly record struct VehicleModelBrandId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleModelBrandId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleModelBrandId));
    }
    public int Value { get; }
}
