using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

// Value Object que encapsula y valida un valor especifico de PersonDocumentTypeId.
public readonly record struct PersonDocumentTypeId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonDocumentTypeId(int value) => Value = ValueObjectValidation.Positive(value, nameof(PersonDocumentTypeId));
    public int Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonDocumentNumber.
public readonly record struct PersonDocumentNumber
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonDocumentNumber(string value) => Value = ValueObjectValidation.Required(value, nameof(PersonDocumentNumber), 30);
    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonFirstName.
public readonly record struct PersonFirstName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonFirstName(string value) => Value = ValueObjectValidation.Required(value, nameof(PersonFirstName), 50);
    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonMiddleName.
public readonly record struct PersonMiddleName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonMiddleName(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PersonMiddleName), 50);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonLastName.
public readonly record struct PersonLastName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonLastName(string value) => Value = ValueObjectValidation.Required(value, nameof(PersonLastName), 50);
    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonSecondLastName.
public readonly record struct PersonSecondLastName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonSecondLastName(string? value) => Value = ValueObjectValidation.Optional(value, nameof(PersonSecondLastName), 50);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonBirthDate.
public readonly record struct PersonBirthDate
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonBirthDate(DateTime? value) => Value = value;
    public DateTime? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de PersonGenderId.
public readonly record struct PersonGenderId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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

// Value Object que encapsula y valida un valor especifico de PersonAddressId.
public readonly record struct PersonAddressId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
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
