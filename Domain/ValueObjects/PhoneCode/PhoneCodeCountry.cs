using Domain.ValueObjects;

namespace Domain.ValueObjects.PhoneCode;

public readonly record struct PhoneCodeCountry
{
    public PhoneCodeCountry(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PhoneCodeCountry), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
