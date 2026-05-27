using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderVehicleId
{
    public ServiceOrderVehicleId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderVehicleId));
    }
    public int Value { get; }
}
