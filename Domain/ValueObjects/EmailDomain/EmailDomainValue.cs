using Domain.ValueObjects;

namespace Domain.ValueObjects.EmailDomain;

// Value Object que encapsula y valida un valor especifico de EmailDomainValue.
public readonly record struct EmailDomainValue
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public EmailDomainValue(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(EmailDomainValue), 100).ToLowerInvariant();
    }
    public string Value { get; }
    public override string ToString() => Value;
}
