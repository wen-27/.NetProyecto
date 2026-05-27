namespace Domain.ValueObjects.PersonEmail;

public readonly record struct PersonEmailIsPrimary
{
    public bool Value { get; }

    public PersonEmailIsPrimary(bool value)
    {
        Value = value;
    }

    public static PersonEmailIsPrimary Primary() => new(true);

    public static PersonEmailIsPrimary Secondary() => new(false);

    public override string ToString() => Value ? "Principal" : "Secundario";
}
