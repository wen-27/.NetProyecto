using Domain.ValueObjects;

namespace Domain.ValueObjects.Person;

// Value Object que encapsula y valida un valor especifico de PersonFirstNames.
public readonly record struct PersonFirstNames
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PersonFirstNames(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(PersonFirstNames), 100);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
