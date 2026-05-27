using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleOwnerHistory;

public readonly record struct VehicleOwnerHistoryCustomerId
{
    public VehicleOwnerHistoryCustomerId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleOwnerHistoryCustomerId));
    }
    public int Value { get; }
}
