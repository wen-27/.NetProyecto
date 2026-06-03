using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehicleTypeId.
public readonly record struct VehicleTypeId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleTypeId));
    }

    public int Value { get; }
}
