using Domain.ValueObjects;

namespace Domain.ValueObjects.DocumentType;

public readonly record struct DocumentTypeCode
{
    public DocumentTypeCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(DocumentTypeCode), 10);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
