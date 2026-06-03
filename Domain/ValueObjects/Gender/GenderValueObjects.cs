using Domain.ValueObjects;

namespace Domain.ValueObjects.Gender;

// Value Object que encapsula y valida un valor especifico de GenderName.
public readonly record struct GenderName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public GenderName(string value) => Value = ValueObjectValidation.Required(value, nameof(GenderName), 50);
    public string Value { get; }
}
