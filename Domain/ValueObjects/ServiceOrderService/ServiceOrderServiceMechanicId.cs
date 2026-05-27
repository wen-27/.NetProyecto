using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrderService;

public readonly record struct ServiceOrderServiceMechanicId
{
    public ServiceOrderServiceMechanicId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(ServiceOrderServiceMechanicId));
    }

    public int Value { get; }
}
