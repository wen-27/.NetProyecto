using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleOwnerHistory;

public readonly record struct VehicleOwnerHistoryPersonId
{
    public VehicleOwnerHistoryPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleOwnerHistoryPersonId));
    }

    public int Value { get; }
}
