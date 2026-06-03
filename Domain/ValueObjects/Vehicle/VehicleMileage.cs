using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

// Value Object que encapsula y valida un valor especifico de VehicleMileage.
public readonly record struct VehicleMileage
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public VehicleMileage(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(VehicleMileage));
    }
    public int Value { get; }
}
