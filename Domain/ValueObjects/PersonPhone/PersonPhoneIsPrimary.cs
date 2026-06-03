namespace Domain.ValueObjects.PersonPhone;

// Value Object que encapsula y valida un valor especifico de PersonPhoneIsPrimary.
public readonly record struct PersonPhoneIsPrimary
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public bool Value { get; }

    public PersonPhoneIsPrimary(bool value)
    {
        Value = value;
    }

    public static PersonPhoneIsPrimary Primary() => new(true);

    public static PersonPhoneIsPrimary Secondary() => new(false);

    public override string ToString() => Value ? "Principal" : "Secundario";
}
