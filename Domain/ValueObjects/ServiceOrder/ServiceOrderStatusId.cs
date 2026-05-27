using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrder;

public readonly record struct ServiceOrderStatusId
{
    public ServiceOrderStatusId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderStatusId));
    }
    public int Value { get; }
}
