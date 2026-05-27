using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrderService;

public readonly record struct ServiceOrderServiceTypeId
{
    public ServiceOrderServiceTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderServiceTypeId));
    }

    public int Value { get; }
}
