using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonDocument;

public readonly record struct PersonDocumentDocumentTypeId
{
    public PersonDocumentDocumentTypeId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonDocumentDocumentTypeId));
    }
    public int Value { get; }
}
