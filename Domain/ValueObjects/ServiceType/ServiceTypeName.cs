using Domain.ValueObjects;

namespace Domain.ValueObjects.ServiceType;

public readonly record struct ServiceTypeName
{
    public ServiceTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(ServiceTypeName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
