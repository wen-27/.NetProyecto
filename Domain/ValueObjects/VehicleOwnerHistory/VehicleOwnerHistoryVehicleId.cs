using Domain.ValueObjects;

namespace Domain.ValueObjects.VehicleOwnerHistory;

public readonly record struct VehicleOwnerHistoryVehicleId
{
    public VehicleOwnerHistoryVehicleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(VehicleOwnerHistoryVehicleId));
    }
    public int Value { get; }
}
