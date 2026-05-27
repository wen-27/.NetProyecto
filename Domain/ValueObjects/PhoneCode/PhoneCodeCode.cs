using Domain.ValueObjects;

namespace Domain.ValueObjects.PhoneCode;

public readonly record struct PhoneCodeCode
{
    public PhoneCodeCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PhoneCodeCode), 10);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
