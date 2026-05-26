using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartCode
{
    public PartCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartCode), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
