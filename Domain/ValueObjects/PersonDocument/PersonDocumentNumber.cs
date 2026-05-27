using Domain.ValueObjects;

namespace Domain.ValueObjects.PersonDocument;

public readonly record struct PersonDocumentNumber
{
    public PersonDocumentNumber(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonDocumentNumber), 50);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
