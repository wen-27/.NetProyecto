using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

public readonly record struct ServiceTypeDescription
{
    public ServiceTypeDescription(string? value)
    {
        Value = ValueObjectValidation.Optional(value, nameof(ServiceTypeDescription), 255);
    }
    public string? Value { get; }
    public override string ToString() => Value ?? string.Empty;
}
