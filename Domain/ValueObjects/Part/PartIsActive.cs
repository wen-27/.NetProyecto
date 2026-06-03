// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PartIsActive, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
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
