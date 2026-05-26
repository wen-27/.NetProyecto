using Domain.ValueObjects;

namespace Domain.ValueObjects.Part;

public readonly record struct PartDescription
{
    public PartDescription(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PartDescription), 255);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
