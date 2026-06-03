namespace Domain.ValueObjects.Part;

// Value Object que encapsula y valida un valor especifico de PartIsActive.
public readonly record struct PartIsActive
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public bool Value { get; }

    public PartIsActive(bool value)
    {
        Value = value;
    }

    public static PartIsActive Active() => new(true);

    public static PartIsActive Inactive() => new(false);

    public override string ToString() => Value ? "Activo" : "Inactivo";
}
