using Domain.ValueObjects;

namespace Domain.ValueObjects.DocumentType;

public readonly record struct DocumentTypeName
{
    public DocumentTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(DocumentTypeName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
