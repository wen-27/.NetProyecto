using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehicleModelId.
public readonly record struct VehicleModelId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleModelId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleModelId));
    }
    public int Value { get; }
}
