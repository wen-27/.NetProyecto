using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

// Value Object que encapsula y valida un valor especifico de PersonLastNames.
public readonly record struct PersonLastNames
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonLastNames(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonLastNames), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
