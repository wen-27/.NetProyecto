using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleMileage
{
    public VehicleMileage(int value)
    {
        Value = ValueObjectValidation.NonNegative(value, nameof(VehicleMileage));
    }
    public int Value { get; }
}
