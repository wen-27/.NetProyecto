using Domain.ValueObjects;

namespace Domain.ValueObjects.EmailDomain;

public readonly record struct EmailDomainValue
{
    public EmailDomainValue(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(EmailDomainValue), 100).ToLowerInvariant();
    }
    public string Value { get; }
    public override string ToString() => Value;
}
