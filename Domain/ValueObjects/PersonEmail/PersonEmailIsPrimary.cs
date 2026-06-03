// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonEmailIsPrimary, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
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
