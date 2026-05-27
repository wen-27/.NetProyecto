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
