using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleTypeId
{
    public VehicleTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleTypeId));
    }

    public int Value { get; }
}
