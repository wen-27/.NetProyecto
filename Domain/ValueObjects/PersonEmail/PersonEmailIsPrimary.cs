namespace Domain.ValueObjects.PersonEmail;

// Value Object que encapsula y valida un valor especifico de PersonEmailIsPrimary.
public readonly record struct PersonEmailIsPrimary
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public bool Value { get; }

    public PersonEmailIsPrimary(bool value)
    {
        Value = value;
    }

    public static PersonEmailIsPrimary Primary() => new(true);

    public static PersonEmailIsPrimary Secondary() => new(false);

    public override string ToString() => Value ? "Principal" : "Secundario";
}
