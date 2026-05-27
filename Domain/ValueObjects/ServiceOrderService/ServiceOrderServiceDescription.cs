using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceOrderService;

public readonly record struct ServiceOrderServiceDescription
{
    public ServiceOrderServiceDescription(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(ServiceOrderServiceDescription), 500);
    }

    public string? Value { get; }
}
