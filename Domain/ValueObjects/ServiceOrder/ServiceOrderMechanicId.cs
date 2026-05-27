using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderMechanicId
{
    public ServiceOrderMechanicId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderMechanicId));
    }
    public int Value { get; }
}
