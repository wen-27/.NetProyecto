using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonDocument;

public readonly record struct PersonDocumentPersonId
{
    public PersonDocumentPersonId(int value)
    {
        Value = ValueObjectValidation.Positive(value, nameof(PersonDocumentPersonId));
    }
    public int Value { get; }
}
