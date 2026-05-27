using Domain.ValueObjects;

namespace Domain.ValueObjects.Vehicle;

public readonly record struct VehicleModelId
{
    public VehicleModelId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleModelId));
    }
    public int Value { get; }
}
