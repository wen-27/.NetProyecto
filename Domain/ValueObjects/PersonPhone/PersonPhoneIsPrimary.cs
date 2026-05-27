namespace Domain.ValueObjects.PersonPhone;

public readonly record struct PersonPhoneIsPrimary
{
    public bool Value { get; }

    public PersonPhoneIsPrimary(bool value)
    {
        Value = value;
    }

    public static PersonPhoneIsPrimary Primary() => new(true);

    public static PersonPhoneIsPrimary Secondary() => new(false);

    public override string ToString() => Value ? "Principal" : "Secundario";
}
