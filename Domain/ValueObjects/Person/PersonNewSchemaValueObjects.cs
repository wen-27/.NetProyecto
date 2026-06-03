// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de PersonNewSchemaValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

public readonly record struct PersonDocumentTypeId
{
    public PersonDocumentTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PersonDocumentTypeId));
    public int Value { get; }
}

public readonly record struct PersonDocumentNumber
{
    public PersonDocumentNumber(string value) => Value = ValueObjectValidation.Required(value, nameof(PersonDocumentNumber), 30);
    public string Value { get; }
}

public readonly record struct PersonFirstName
{
    public PersonFirstName(string value) => Value = ValueObjectValidation.Required(value, nameof(PersonFirstName), 50);
    public string Value { get; }
}

public readonly record struct PersonMiddleName
{
    public PersonMiddleName(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PersonMiddleName), 50);
    public string? Value { get; }
}

public readonly record struct PersonLastName
{
    public PersonLastName(string value) => Value = ValueObjectValidation.Required(value, nameof(PersonLastName), 50);
    public string Value { get; }
}

public readonly record struct PersonSecondLastName
{
    public PersonSecondLastName(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PersonSecondLastName), 50);
    public string? Value { get; }
}

public readonly record struct PersonBirthDate
{
    public PersonBirthDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}

public readonly record struct PersonGenderId
{
    public PersonGenderId(int? value)
    {
        if (value.HasValue && value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El genero debe ser mayor que cero.");
        }

        Value = value;
    }

    public int? Value { get; }
}

public readonly record struct PersonAddressId
{
    public PersonAddressId(int? value)
    {
        if (value.HasValue && value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La direccion debe ser mayor que cero.");
        }

        Value = value;
    }

    public int? Value { get; }
}
