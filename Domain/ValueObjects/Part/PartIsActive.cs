namespace Domain.ValueObjects.Part;

public readonly record struct PartIsActive
{
    public bool Value { get; }

    public PartIsActive(bool value)
    {
        Value = value;
    }

    public static PartIsActive Active() => new(true);

    public static PartIsActive Inactive() => new(false);

    public override string ToString() => Value ? "Activo" : "Inactivo";
}
