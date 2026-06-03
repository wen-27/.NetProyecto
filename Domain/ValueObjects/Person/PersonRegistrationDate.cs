// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonRegistrationDate, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
namespace Domain.ValueObjects.Person;

public readonly record struct PersonRegistrationDate
{
    public DateTime Value { get; }

    public PersonRegistrationDate(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de registro es obligatoria.", nameof(value));
        }

        if (value > DateTime.UtcNow.AddMinutes(5))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La fecha de registro no puede estar en el futuro.");
        }

        Value = value;
    }

    public override string ToString() => Value.ToString("O");
}
