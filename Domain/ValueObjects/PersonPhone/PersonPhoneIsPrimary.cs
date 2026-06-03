// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonPhoneIsPrimary, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
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
