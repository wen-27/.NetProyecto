namespace Domain.ValueObjects.PersonDocument;

public readonly record struct PersonDocumentIsPrimary
{
    public bool Value { get; }

    public PersonDocumentIsPrimary(bool value)
    {
        Value = value;
    }

    public static PersonDocumentIsPrimary Primary() => new(true);

    public static PersonDocumentIsPrimary Secondary() => new(false);

    public override string ToString() => Value ? "Principal" : "Secundario";
}
