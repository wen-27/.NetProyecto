using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrderService;

public readonly record struct ServiceOrderServiceOrderId
{
    public ServiceOrderServiceOrderId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderServiceOrderId));
    }

    public int Value { get; }
}
